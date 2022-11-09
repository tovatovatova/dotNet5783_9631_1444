

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    public int Add(Order newOrder)
    {
        newOrder.OrderId = DataSource.Config.NextOrderNumber;
        DataSource.OrderList.Add(newOrder);
        return newOrder.OrderId;
    }
    public Order ReadById(int orderId)
    {
        Order? newOrder=DataSource.OrderList.Find(x => x.Value.OrderId ==orderId);    
        if(newOrder!= null)
        {
            return (Order)newOrder;

        }
        throw new Exception("order not exist");
    }
    public IEnumerable<Order?> GetAll() => DataSource.OrderList;
    public void Delete(int orderId)
    {
        Order? newOrder = DataSource.OrderList.Find(x => x.Value.OrderId == orderId);
        if( newOrder== null)
            throw new Exception("order not exist");
        DataSource.OrderList.Remove(newOrder);
    }
    public void Update(Order newOrder)
    {
       int index= DataSource.OrderList.FindIndex(x => x.Value.OrderId == newOrder.OrderId);
        if(index==-1)
            throw new Exception("order not exist");
        DataSource.OrderList[index] = newOrder;
    }
}
