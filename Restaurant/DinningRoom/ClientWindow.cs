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
        List <Order> orders;
        List <Table> tables;
        List <Product> products;
        delegate ListViewItem LVAddDelegate(ListViewItem lvOrder);
        delegate void ChCommDelegate(Order order);

        public ClientWindow()
        {
            RemotingConfiguration.Configure("DinningRoom.exe.config", false);
            InitializeComponent();
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));

            startTablesComboBox();
            startProductsComboBox();
            updateOrdersListView();

            evRepeater = new AlterEventRepeater();
            //evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            //listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
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
                this.productsComboBox.Items.Add(product.Name);
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
                ListViewItem listItem = new ListViewItem(new string[] { orders[i].Id.ToString(), orders[i].TableId.ToString(), orders[i].Product.Name, orders[i].Quantity.ToString(), orders[i].StateProperty.ToString() });
                ordersListView.Items.Add(listItem);
                if (orders[i].StateProperty.Equals(Order.State.NOT_PROCESSED))
                {
                    ordersListView.Items[i].BackColor = Color.Red;
                }
                else if (orders[i].StateProperty.Equals(Order.State.PROCESSING))
                {
                    ordersListView.Items[i].BackColor = Color.Yellow;
                }
                else if (orders[i].StateProperty.Equals(Order.State.FINISHED))
                {
                    ordersListView.Items[i].BackColor = Color.Green;
                }
                else if (orders[i].StateProperty.Equals(Order.State.DELIVERED))
                {
                    ordersListView.Items[i].BackColor = Color.Turquoise;
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

        
        private void addOrderButton_Click(object sender, EventArgs e)
        {
            int tableID = Int32.Parse(tablesComboBox.SelectedItem.ToString());
            String productName = productsComboBox.SelectedItem.ToString();
            Product product = listServer.getProducts().Find(p => p.Name.Equals(productName));
            int qnt = Int32.Parse(qntTextBox.Text);

            Order order = new Order(tableID, product, qnt);
        }


        private void ClientWindow_Load(object sender, EventArgs e)
        {

        }

        private void deliveredButton_click(object sender, EventArgs e)
        {

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