using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class ListSingleton : MarshalByRefObject, IListSingleton {
    ArrayList orders;
    public event AlterDelegate alterEvent;
    int type = 1;
    public ListSingleton()
    {
        Console.WriteLine("Constructor called.");
        orders = new ArrayList();
        Order order = new Order(1, 10, new Product(100, "cocacola", 5), 2);
        orders.Add(order);
    }

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public ArrayList getOrders()
    {
        Console.WriteLine("getOrders() called.");
        return orders;
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

