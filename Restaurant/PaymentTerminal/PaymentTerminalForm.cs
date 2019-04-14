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
        int nTablesServed = 0;
        delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void LVUpdateDelegate();
        delegate void ChCommDelegate(Order order);

        public PaymentTerminalForm()
        {
            RemotingConfiguration.Configure("PaymentTerminal.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));
            updateTablesListView();
            evRepeater = new AlterEventRepeater();
            evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
        }

        public void updateTablesListView()
        {
            tables = new List<Table>();
            tables = listServer.getTables(Table.State.DONE);
                       
            tablesListView.Items.Clear();
            for (int i = 0; i < tables.Count; i++)
            {             
                List<Order> tableOrders = listServer.getOrdersByTable(tables[i].Id, Order.State.DELIVERED);
                float totalValue = 0;

                foreach(Order order in tableOrders)
                {
                    totalValue += order.TotalPrice;
                }

                ListViewItem listItem = new ListViewItem(new string[] { tables[i].Id.ToString(), totalValue.ToString()+"€" });
                if(totalValue != 0)
                    tablesListView.Items.Add(listItem);
            }

            //Total Value Collected
            List<Order> orders = listServer.getOrders(Order.State.CLOSED);
            float value = 0F;
            for (int i = 0; i < orders.Count; i++)
            {
                value = value + orders[i].TotalPrice;
            }
            totalValue.Text = value.ToString();
            tablesServed.Text = nTablesServed.ToString();

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (tablesListView.CheckedItems != null)
            {
                for (int i = 0; i < tablesListView.CheckedItems.Count; i++)
                {
                    String tableId = tablesListView.CheckedItems[i].Text.Substring(0, 1);
                    Table table = listServer.getTables(Table.State.DONE).Find(o => o.Id.ToString().Equals(tableId));

                    if (table != null)
                    {
                        listServer.changeTableStatus(Int32.Parse(tableId), Table.State.CLOSED);
                        Console.WriteLine("CLOSED " + table.Id);
                        nTablesServed++;
                        //TODO: PRINT RECIBO
                    }
                }
            }
        }

        /* The client is also a remote object. The Server calls remotely the AlterEvent handler  *
      * Infinite lifetime                                                                     */

        public override object InitializeLifetimeService()
        {
            return null;
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */


        public void DoAlterations(Operation op, Order order, int tableId)
        {

            LVUpdateDelegate lvUpdate;

            switch (op)
            {
                case Operation.Changed_Table_State:
                    Console.WriteLine("Changed Table State!");
                    lvUpdate = new LVUpdateDelegate(updateTablesListView);
                    BeginInvoke(lvUpdate);
                    break;
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
