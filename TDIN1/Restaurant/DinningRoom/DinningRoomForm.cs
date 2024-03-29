﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace DinningRoom
{

    public partial class DinningRoomForm : Form
    {
        IListSingleton listServer;
        AlterEventRepeater evRepeater;
        List<Order> orders;
        List<Table> tables;
        List<Product> products;
        //delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void LVUpdateDelegate();
        delegate void ChCommDelegate(Order order);

        public DinningRoomForm()
        {
            RemotingConfiguration.Configure("DinningRoom.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));

            startTablesComboBox();
            startProductsComboBox();
            updateOrdersListView();

            evRepeater = new AlterEventRepeater();
            evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
        }

        public void startTablesComboBox()
        {
            tables = listServer.getTables();
            foreach (var table in tables)
            {
                this.tablesComboBox.Items.Add(table.Id.ToString());
            }
        }

        public void startProductsComboBox()
        {
            products = listServer.getProducts();
            foreach (var product in products)
            {
                this.productsComboBox.Items.Add(product.Name + " - " + product.Price + "€");
            }
        }

        public void updateOrdersListView()
        {
            List<Order> ordersNP = listServer.getOrders(Order.State.NOT_PROCESSED);
            List<Order> ordersP = listServer.getOrders(Order.State.PROCESSING);
            List<Order> ordersF = listServer.getOrders(Order.State.FINISHED);
            List<Order> ordersD = listServer.getOrders(Order.State.DELIVERED);
            orders = new List<Order>();
            orders.AddRange(ordersNP);
            orders.AddRange(ordersP);
            orders.AddRange(ordersF);
            orders.AddRange(ordersD);

            ordersListView.Items.Clear();
            for (int i = 0; i < orders.Count; i++)
            {
                ListViewItem listItem = new ListViewItem(new string[] { orders[i].Id.ToString(), orders[i].TableId.ToString(), orders[i].Product.Name, orders[i].Quantity.ToString(), orders[i].TotalPrice.ToString() + "€", orders[i].StateProperty.ToString() });
                ordersListView.Items.Add(listItem);
                if (orders[i].StateProperty.Equals(Order.State.NOT_PROCESSED))
                {
                    ordersListView.Items[i].BackColor = Color.LightCoral;
                }
                else if (orders[i].StateProperty.Equals(Order.State.PROCESSING))
                {
                    ordersListView.Items[i].BackColor = Color.Gold;
                }
                else if (orders[i].StateProperty.Equals(Order.State.FINISHED))
                {
                    ordersListView.Items[i].BackColor = Color.YellowGreen;
                }
                else if (orders[i].StateProperty.Equals(Order.State.DELIVERED))
                {
                    ordersListView.Items[i].BackColor = Color.CornflowerBlue;
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
            //LVAddDelegate lvAdd;
            LVUpdateDelegate lvUpdate;
            //ChCommDelegate chComm;

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
                    //updateOrdersListView();
                    break;
            }
        }

        private void addOrderButton_Click(object sender, EventArgs e)
        {
            if (tablesComboBox.SelectedItem != null && productsComboBox.SelectedItem != null)
            {
                int tableID = Int32.Parse(tablesComboBox.SelectedItem.ToString());
                String productName = productsComboBox.SelectedItem.ToString().Substring(0, productsComboBox.SelectedItem.ToString().LastIndexOf(" - "));
                Console.WriteLine(productName);
                Product product = listServer.getProducts().Find(p => p.Name.Equals(productName));
                int qnt = Int32.Parse(qntTextBox.Text);

                Order order = new Order(tableID, product, qnt);

                listServer.addOrder(order);
            }
        }


        private void deliveredButton_click(object sender, EventArgs e)
        {
            if (ordersListView.CheckedItems != null)
            {
                for (int i = 0; i < ordersListView.CheckedItems.Count; i++)
                {
                    String orderID = ordersListView.CheckedItems[i].Text.Substring(0, 36);
                    Order order = listServer.getOrders(Order.State.FINISHED).Find(o => o.Id.ToString().Equals(orderID));

                    if (order != null)
                    {
                        Console.WriteLine(order.Id + " " + order.TableId + " " + order.Product.Name + " " + order.TotalPrice);
                        listServer.changeOrderStatus(order.Id, Order.State.DELIVERED);
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