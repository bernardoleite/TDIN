using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace PaymentTerminal
{
    public partial class PaymentTerminalForm : Form
    {
        IListSingleton listServer;
        AlterEventRepeater evRepeater;
        List<Table> tables;
        delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void ChCommDelegate(Order order);

        public PaymentTerminalForm()
        {
            RemotingConfiguration.Configure("PaymentTerminal.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));
            updateTablesListView();
            evRepeater = new AlterEventRepeater();
            //evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            //listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
        }

        public void updateTablesListView()
        {
            tables = new List<Table>();

            List<Table> tablesD = listServer.getTables(Table.State.DONE);
            tables.AddRange(tablesD);
           
            tablesListView.Items.Clear();
            for (int i = 0; i < tables.Count; i++)
            {             
                List<Order> tableOrders = listServer.getOrdersByTable(tables[i].Id);
                float totalValue = 0;

                foreach(Order order in tableOrders)
                {
                    totalValue += order.TotalPrice;
                }

                ListViewItem listItem = new ListViewItem(new string[] { tables[i].Id.ToString(), totalValue.ToString()+"€" });
                tablesListView.Items.Add(listItem);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {

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
                /*case Operation.New:
                    //lvAdd = new LVAddDelegate(ordersListView.Items.Add);
                    ListViewItem lvOrder = new ListViewItem(new string[] { order.Type.ToString(), order.TableId.ToString() });
                    BeginInvoke(lvAdd, new object[] { lvOrder });
                    break;
                case Operation.Change:
                    chComm = new ChCommDelegate(ChangeAItem);
                    BeginInvoke(chComm, new object[] { order });
                    break;*/
            }
        }

        private void ChangeAItem(Order it)
        {
            /*foreach (ListViewItem lvI in ordersListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Type)
                {
                    lvI.SubItems[2].Text = it.TableId.ToString();
                    break;
                }*/
        }


        /* Client interface event handlers */
        private void ClientWindow_Load(object sender, EventArgs e)
        {

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
}
