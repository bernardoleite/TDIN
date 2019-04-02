﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum Operation { New, Change };

public delegate void AlterDelegate(Operation op, Order order);

public interface IListSingleton
{
    event AlterDelegate alterEvent;

    List<Table> getTables();
    List<Product> getProducts();
    List<Order> getOrders(Order.State state);
    int GetNewType();
    void addOrder(Order order);
    void changeStatus(int type, Order.State newStatus);
}

public class AlterEventRepeater : MarshalByRefObject
{
    public event AlterDelegate alterEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(Operation op, Order order)
    {
        if (alterEvent != null)
            alterEvent(op, order);
    }
}