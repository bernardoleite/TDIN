using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DinningRoom
{

    public partial class ClientWindow : Form
    {
        IListSingleton listServer;
        AlterEventRepeater evRepeater;
        ArrayList orders;
        delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void ChCommDelegate(Order order);

        public ClientWindow()
        {
            RemotingConfiguration.Configure("Client.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));
            orders = listServer.getOrders();
            evRepeater = new AlterEventRepeater();
            //evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
        }

        /* The client is also a remote object. The Server calls remotely the AlterEvent handler  *
        * Infinite lifetime                                                                     */

        public override object InitializeLifetimeService()
        {
            return null;
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */


        public void DoAlterations(Operation op, Order order)
        {
            LVAddDelegate lvAdd;
            ChCommDelegate chComm;

            switch (op)
            {
                case Operation.New:
                    lvAdd = new LVAddDelegate(ordersListView.Items.Add);
                    ListViewItem lvOrder = new ListViewItem(new string[] { order.Type.ToString(), order.tableId.ToString() });
                    BeginInvoke(lvAdd, new object[] { lvOrder });
                    break;
                case Operation.Change:
                    chComm = new ChCommDelegate(ChangeAItem);
                    BeginInvoke(chComm, new object[] { order });
                    break;
            }
        }


        private void ChangeAItem(Order it)
        {
            foreach (ListViewItem lvI in ordersListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Type)
                {
                    lvI.SubItems[2].Text = it.tableId.ToString();
                    break;
                }
        }

        /* Client interface event handlers */

    }

    /* Mechanism for instanciating a remote object through its interface, using the config file */

    class RemoteNew
    {
        private static Hashtable types = null;

        private static void InitTypeTable()
        {
            types = new Hashtable();
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
                types.Add(entry.ObjectType, entry);
        }

        public static object New(Type type)
        {
            if (types == null)
                InitTypeTable();
            WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry)types[type];
            if (entry == null)
                throw new RemotingException("Type not found!");
            return RemotingServices.Connect(type, entry.ObjectUrl);
        }
    }

}