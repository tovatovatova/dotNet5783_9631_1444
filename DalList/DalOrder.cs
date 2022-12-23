

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
        return DataSource.OrderList.FirstOrDefault(order => order?.OrderId == orderId) ?? throw new DalIdDoNotExistException(orderId, "order");
        //Order? newOrder=DataSource.OrderList.Find(x => x?.OrderId ==orderId);    
        //if(newOrder!= null)
        //{
        //    return (Order)newOrder;

        //}
        //throw new Exception("order not exist");
    }
    /// <summary>
    /// deletes order according to the given id
    /// </summary>
    /// <param name="orderId"></param>
    /// <exception cref="Exception">throw exeption when the the order not found</exception>
    public void Delete(int orderId)
    {
        Order? delOrder= DataSource.OrderList.FirstOrDefault(order => order?.OrderId == orderId) ?? throw new DalIdDoNotExistException(orderId, "order");
        DataSource.OrderList.Remove(delOrder);
    }
    /// <summary>
    /// update an order in its spesific index according to the given order
    /// </summary>
    /// <param name="newOrder"></param>
    /// <exception cref="Exception">throw exeption when the the order not found</exception>
    public void Update(Order newOrder)
    {
        Order? delOrder = DataSource.OrderList.FirstOrDefault(order => order?.OrderId == newOrder.OrderId) ?? throw new DalIdDoNotExistException(newOrder.OrderId, "order");
        int index = DataSource.OrderList.FindIndex(x => x?.OrderId == newOrder.OrderId);
        DataSource.OrderList[index] = newOrder;
    }
    /// <summary>
    /// runs on the list and add to a new list
    /// </summary>
    /// <returns>newList</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.OrderList.Where(item => filter(item));
        return DataSource.OrderList.Select(item => item);
    }

    

    
}
