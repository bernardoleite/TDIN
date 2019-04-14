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
        public enum Type {KITCHEN, BAR};
        public Product.Type ProductType;
        delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void LVUpdateDelegate();
        delegate void ChCommDelegate(Order order);

        public KitchenBarForm(KitchenBarForm.Type type)
        {
            RemotingConfiguration.Configure("KitchenBar.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));

            if (type.Equals(Type.KITCHEN))
            {
                this.ProductType = Product.Type.FOOD;
            }
                
            else if (type.Equals(Type.BAR))
                this.ProductType = Product.Type.DRINK;

            updateOrdersListView();
            evRepeater = new AlterEventRepeater();
            evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);

        }

        public void updateOrdersListView()
        {
            orders = new List<Order>();
            
            List<Order> ordersNP = listServer.getOrdersByType(Order.State.NOT_PROCESSED, this.ProductType);
            List<Order> ordersP = listServer.getOrdersByType(Order.State.PROCESSING, this.ProductType);
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


        public void DoAlterations(Operation op, Order order, int tableId)
        {

            LVUpdateDelegate lvUpdate;

            switch (op)
            {
                case Operation.Added_Order:
                    Console.WriteLine("Added new Order!");
                    lvUpdate = new LVUpdateDelegate(updateOrdersListView);
                    BeginInvoke(lvUpdate);
                    break;
                case Operation.Changed_Order_State:
                    Console.WriteLine("Changed Order State!");
                    lvUpdate = new LVUpdateDelegate(updateOrdersListView);
                    BeginInvoke(lvUpdate);
                    break;
                case Operation.Changed_Table_State:
                    Console.WriteLine("Changed Table State!");
                    lvUpdate = new LVUpdateDelegate(updateOrdersListView);
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

        private void processingButton_click(object sender, EventArgs e)
        {
            if (ordersListView.CheckedItems != null)
            {
                for (int i = 0; i < ordersListView.CheckedItems.Count; i++)
                {
                    String orderID = ordersListView.CheckedItems[i].Text.Substring(0, 36);

                    Order order = listServer.getOrdersByType(Order.State.NOT_PROCESSED, this.ProductType).Find(o => o.Id.ToString().Equals(orderID));

                    if (order != null)
                    {
                        Console.WriteLine(order.Id + " " + order.TableId + " " + order.Product.Name + " " + order.TotalPrice);
                        //TODO: CHANGE ORDER STATE EVENT
                        listServer.changeOrderStatus(order.Id, Order.State.PROCESSING); 
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

                    Order order = listServer.getOrdersByType(Order.State.PROCESSING, this.ProductType).Find(o => o.Id.ToString().Equals(orderID));

                    if (order != null)
                    {
                        Console.WriteLine(order.Id + " " + order.TableId + " " + order.Product.Name + " " + order.TotalPrice);
                        //TODO: CHANGE ORDER STATE EVENT
                        listServer.changeOrderStatus(order.Id, Order.State.FINISHED);
                    }
                }
            }
        }

        private void ordersListView_SelectedIndexChanged(object sender, EventArgs e)
        {

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
