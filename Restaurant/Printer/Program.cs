using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Printer
{
    class Program
    {
        IListSingleton listServer;
        AlterEventRepeater evRepeater;
        List<Table> tables;
        int nTablesServed = 0;
        delegate void LVUpdateDelegate();
        delegate void ChCommDelegate(Order order);

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            Console.WriteLine("hello im there");
            RemotingConfiguration.Configure("Printer.exe.config", false);
            listServer = (IListSingleton)RemoteNew.New(typeof(IListSingleton));
            evRepeater = new AlterEventRepeater();
            evRepeater.alterEvent += new AlterDelegate(DoAlterations);
            listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
            tables = listServer.getTables();

            Console.ReadLine();
        }



        public void DoAlterations(Operation op, Order order, int tableId)
        {

            switch (op)
            {
                case Operation.Changed_Table_State:
                    if(tableId!=0)
                        printBill(tableId);
                    break;
            }
        }

        public void printBill(int tableId)
        {
            tables = listServer.getTables();

            Table table = tables.Find(t => t.Id.Equals(tableId));

            if (table != null && table.StateProperty.Equals(Table.State.CLOSED))
            {
                List<Order> tableOrders = listServer.getOrdersByTable(tableId, Order.State.DELIVERED);

                Console.WriteLine("Bill for Table: " +  table.Id);
                for (int j = 0; j < tableOrders.Count; j++)
                {
                    listServer.changeOrderStatus(tableOrders[j].Id, Order.State.CLOSED);
                    Console.WriteLine(tableOrders[j].Id + " " + tableOrders[j].TableId + " " + tableOrders[j].Product.Name + " " + tableOrders[j].TotalPrice);
                }
            }

        }


    }


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
