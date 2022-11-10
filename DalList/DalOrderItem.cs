

using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// Add
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns></returns>
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.OrderItemId = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemList.Add(newOrderItem);
        return newOrderItem.OrderItemId;
    }
    /// <summary>
    /// Request
    /// </summary>
    /// <param name="orderItemId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetById(int orderItemId)
    {
        OrderItem? newOrder = DataSource.OrderItemList.Find(x => x?.OrderItemId == orderItemId);
        if (newOrder != null)
        {
            return (OrderItem)newOrder;

        }
        throw new Exception("order item not exist");
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="orderItemId"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int orderItemId)
    {
        OrderItem? newOrder = DataSource.OrderItemList.Find(x => x?.OrderId == orderItemId);
        if (newOrder == null)
            throw new Exception("order item not exist");
        DataSource.OrderItemList.Remove(newOrder);
    }
    /// <summary>
    /// UpDate
    /// </summary>
    /// <param name="newOrder"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem newOrder)
    {
        int index = DataSource.OrderItemList.FindIndex(x => x?.OrderItemId == newOrder.OrderItemId);
        if (index == -1)
            throw new Exception("order itemnot exist");
        DataSource.OrderItemList[index] = newOrder;
    }
    /// <summary>
    /// Get All
    /// </summary>
    /// <returns></returns>
     public IEnumerable<OrderItem?> GetAll()
    {
        List<OrderItem?> newList=new List<OrderItem?>();
        DataSource.OrderItemList.ForEach(x=> newList.Add(x));
        return newList;
    }
    public List<OrderItem?> GetItemsInOrder(int orderId)
    {
        List<OrderItem?> itemsInOrder = new List<OrderItem?>();
        DataSource.OrderItemList.FindAll(x => x?.OrderId == orderId);
        return itemsInOrder;
    }
    public OrderItem GetItemByOrderAndProduct(int orderId,int productId)
    {
        OrderItem orderItem=DataSource.OrderItemList.Find(x=> (x?.OrderId==orderId)&&(x?.ProductId==productId)) ?? throw new Exception("This product doesn't exist");
        return orderItem;
    }
}

