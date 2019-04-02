using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class ListSingleton : MarshalByRefObject, IListSingleton {
    List <Order> orders;
    List <Table> tables;
    List <Product> products;

    public event AlterDelegate alterEvent;
    int type = 1;
    public ListSingleton()
    {
        Console.WriteLine("Constructor called.");

        //TABLES
        tables = new List<Table>();
        for (int i = 0; i < 10; i++)
        {
            tables.Add(new Table(i));
        }

        //PRODUCTS
        products = new List<Product>();
        products.Add(new Product(0, "Cocacola", 5, Product.Type.Drink));
        products.Add(new Product(1, "Icetea", 5, Product.Type.Drink));
        products.Add(new Product(2, "Water", 5, Product.Type.Drink));
        products.Add(new Product(3, "Croissant", 5, Product.Type.Food));
        products.Add(new Product(4, "Hamburguer", 5, Product.Type.Food));
        products.Add(new Product(5, "Fish", 5, Product.Type.Food));

        //ORDERS
        orders = new List<Order>();
        Order order0 = new Order(0, 9, products[0], 2, Order.State.NOT_PROCESSED);
        Order order1 = new Order(1, 8, products[1], 2, Order.State.PROCESSING);
        Order order2 = new Order(2, 7, products[2], 2, Order.State.FINISHED);
        Order order3 = new Order(3, 5, products[3], 2, Order.State.DELIVERED);
        Order order4 = new Order(4, 2, products[5], 2, Order.State.CLOSED);
        orders.Add(order0);
        orders.Add(order1);
        orders.Add(order2);
        orders.Add(order3);
        orders.Add(order4);
    }

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public List<Table> getTables() //TODO: RECEBER ESTADO
    {
        Console.WriteLine("getTables() called.");
        return tables;
    }

    public List<Product> getProducts()
    {
        Console.WriteLine("getProducts() called.");
        return products;
    }

    public List<Order> getOrders(Order.State state)
    {
        Console.WriteLine("getOrders(state) called.");
        List<Order> res = new List<Order>();
        foreach(Order order in orders)
        {
            if (order.StateProperty.Equals(state))
                res.Add(order);
        }
        return res;
    }

    public int GetNewType()
    {
        return type++;
    }

    public void addOrder(Order order)
    {
        orders.Add(order);
        NotifyClients(Operation.New, order);
    }

    public void changeStatus(int type, Order.State newStatus)
    {
        Order norder = null;

        foreach (Order it in orders)
        {
            if (it.Type == type)
            {
                it.StateProperty = newStatus;
                norder = it;
                break;
            }
        }
        NotifyClients(Operation.Change, norder);
    }

    void NotifyClients(Operation op, Order order)
    {
        if (alterEvent != null)
        {
            Delegate[] invkList = alterEvent.GetInvocationList();

            foreach (AlterDelegate handler in invkList)
            {
                new Thread(() => {
                    try
                    {
                        handler(op, order);
                        Console.WriteLine("Invoking event handler");
                    }
                    catch (Exception)
                    {
                        alterEvent -= handler;
                        Console.WriteLine("Exception: Removed an event handler");
                    }
                }).Start();
            }
        }
    }



}

