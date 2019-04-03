using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace KitchenBar
{
    public partial class KitchenBarForm : Form
    {
        IListSingleton listServer;
        AlterEventRepeater evRepeater;
        List<Order> orders;
        delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void ChCommDelegate(Order order);

        public KitchenBarForm()
        {
            RemotingConfiguration.Configure("KitchenBar.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));

            updateOrdersListView();

            evRepeater = new AlterEventRepeater();
            //evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            //listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);

        }

        public void updateOrdersListView()
        {
            List<Order> ordersNP = listServer.getOrders(Order.State.NOT_PROCESSED);
            List<Order> ordersP = listServer.getOrders(Order.State.PROCESSING);
            orders = new List<Order>();
            orders.AddRange(ordersNP);
            orders.AddRange(ordersP);
            
            ordersListView.Items.Clear();
            for (int i = 0; i < orders.Count; i++)
            {
                ListViewItem listItem = new ListViewItem(new string[] { orders[i].Id.ToString(), orders[i].TableId.ToString(), orders[i].Product.Name, orders[i].Quantity.ToString(), orders[i].StateProperty.ToString() });
                ordersListView.Items.Add(listItem);
                if (orders[i].StateProperty.Equals(Order.State.NOT_PROCESSED))
                {
                    ordersListView.Items[i].BackColor = Color.LightCoral;
                }
                else if (orders[i].StateProperty.Equals(Order.State.PROCESSING))
                {
                    ordersListView.Items[i].BackColor = Color.Gold;
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

        private void processingButton_click(object sender, EventArgs e)
        {
            if (ordersListView.CheckedItems != null)
            {
                for (int i = 0; i < ordersListView.CheckedItems.Count; i++)
                {
                    String orderID = ordersListView.CheckedItems[i].Text.Substring(0, 36);
                    Order order = listServer.getOrders(Order.State.NOT_PROCESSED).Find(o => o.Id.ToString().Equals(orderID));

                    if (order != null)
                    {
                        Console.WriteLine(order.Id + " " + order.TableId + " " + order.Product.Name + " " + order.TotalPrice);
                        //TODO: CHANGE ORDER STATE EVENT
                    }
                }
            }
        }

        private void finishedButton_click(object sender, EventArgs e)
        {
            if (ordersListView.CheckedItems != null)
            {
                for (int i = 0; i < ordersListView.CheckedItems.Count; i++)
                {
                    String orderID = ordersListView.CheckedItems[i].Text.Substring(0, 36);
                    Order order = listServer.getOrders(Order.State.PROCESSING).Find(o => o.Id.ToString().Equals(orderID));

                    if (order != null)
                    {
                        Console.WriteLine(order.Id + " " + order.TableId + " " + order.Product.Name + " " + order.TotalPrice);
                        //TODO: CHANGE ORDER STATE EVENT
                    }
                }
            }
        }
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
