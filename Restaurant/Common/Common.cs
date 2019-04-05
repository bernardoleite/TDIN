using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Operation { Added_Order, Changed_Order_State, Changed_Table_State };

public delegate void AlterDelegate(Operation op, Order order);

public interface IListSingleton
{
    event AlterDelegate alterEvent;

    List<Table> getTables(Table.State state);
    List<Product> getProducts();
    List<Order> getOrders(Order.State state);
    List<Order> getOrdersByType(Order.State state, Product.Type type);
    List<Order> getOrdersByTable(int tableId);
    void addOrder(Order order);
    //void changeStatus(int type, Order.State newStatus);
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