

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    public int Add(Order newOrder)
    {
        newOrder.OrderId = DataSource.Config.NextOrderNumber;
        DataSource.OrderList.Add(newOrder);
        return newOrder.OrderId;
    }
    /// <summary>
    ///Request
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetById(int orderId)
    {
        Order? newOrder=DataSource.OrderList.Find(x => x?.OrderId ==orderId);    
        if(newOrder!= null)
        {
            return (Order)newOrder;

        }
        throw new Exception("order not exist");
    }
    /// <summary>
    /// Get All
    /// </summary>
    /// <returns></returns>
    public void Delete(int orderId)
    {
        Order? newOrder = DataSource.OrderList.Find(x => x?.OrderId == orderId);
        if( newOrder== null)
            throw new Exception("order not exist");
        DataSource.OrderList.Remove(newOrder);
    }
    public void Update(Order newOrder)
    {
       int index= DataSource.OrderList.FindIndex(x => x?.OrderId == newOrder.OrderId);
        if(index==-1)
            throw new Exception("order not exist");
        DataSource.OrderList[index] = newOrder;
    }
    /// <summary>
    /// Get All
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> newList = new List<Order?>();
        DataSource.OrderList.ForEach(x => newList.Add(x));
        return newList;
    }
}
