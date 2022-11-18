

using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// the method add new order to list of orders
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>newOrder.OrderId</returns>
    public int Add(Order newOrder)
    {
        newOrder.OrderId = DataSource.Config.NextOrderNumber;
        DataSource.OrderList.Add(newOrder);
        return newOrder.OrderId;
    }
    /// <summary>
    /// the method returns the Order with the given id
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>newOrder</returns>
    /// <exception cref="Exception">throw exeption when the the order not found</exception>
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
    /// deletes order according to the given id
    /// </summary>
    /// <param name="orderId"></param>
    /// <exception cref="Exception">throw exeption when the the order not found</exception>
    public void Delete(int orderId)
    {
        Order? newOrder = DataSource.OrderList.Find(x => x?.OrderId == orderId);
        if( newOrder== null)
            throw new Exception("order not exist");
        DataSource.OrderList.Remove(newOrder);
    }
    /// <summary>
    /// update an order in its spesific index according to the given order
    /// </summary>
    /// <param name="newOrder"></param>
    /// <exception cref="Exception">throw exeption when the the order not found</exception>
    public void Update(Order newOrder)
    {
       int index= DataSource.OrderList.FindIndex(x => x?.OrderId == newOrder.OrderId);
        if(index==-1)
            throw new Exception("order not exist");
        DataSource.OrderList[index] = newOrder;
    }
    /// <summary>
    /// runs on the list and add to a new list
    /// </summary>
    /// <returns>newList</returns>
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> newList = new List<Order?>();
        DataSource.OrderList.ForEach(x => newList.Add(x));
        return newList;
    }

    

    
}
