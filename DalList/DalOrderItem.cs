

using DO;
using System.Security.Cryptography.X509Certificates;
using DalApi;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Add a new order item to the list 
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns>newOrderItem.OrderItemId</returns>
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.OrderItemId = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemList.Add(newOrderItem);
        return newOrderItem.OrderItemId;
    }
    /// <summary>
    /// return order item with the given order item id
    /// </summary>
    /// <param name="orderItemId"></param>
    /// <returns>(OrderItem)newOrder</returns>
    /// <exception cref="Exception">throw exeption if order item doesnt exist</exception>
    public OrderItem GetById(int orderItemId)
    {
        return  DataSource.OrderItemList.FirstOrDefault(orderItem => orderItem.Value.OrderItemId==orderItemId) ?? throw new DalIdDoNotExistException(orderItemId, "orderItem");

        //OrderItem? newOrder = DataSource.OrderItemList.Find(x => x?.OrderItemId == orderItemId);
        //if (newOrder != null)
        //{
        //    return (OrderItem)newOrder;

        //}
        //throw new Exception("order item is not exist");
    }
    /// <summary>
    /// Delete item with the given order item id
    /// </summary>
    /// <param name="orderItemId"></param>
    /// <exception cref="Exception">throw exeption if order item doesnt exist</exception>
    public void Delete(int orderItemId)
    {
        OrderItem? delOrderItem = DataSource.OrderItemList.FirstOrDefault(orderItem => orderItem.Value.OrderItemId == orderItemId) ?? throw new DalIdDoNotExistException(orderItemId, "orderItem");
        DataSource.OrderItemList.Remove(delOrderItem);
    }
    /// <summary>
    /// UpDate a spesific order item in a spsific index with the given new order
    /// </summary>
    /// <param name="newOrder"></param>
    /// <exception cref="Exception">throw exeption if order item doesnt exist</exception>
    public void Update(OrderItem newOrder)
    {
        OrderItem? uplOrder = DataSource.OrderItemList.FirstOrDefault(orderItem => orderItem.Value.OrderItemId == newOrder.OrderItemId) ?? throw new DalIdDoNotExistException(newOrder.OrderItemId, "orderItem");
        int index = DataSource.OrderItemList.FindIndex(x => x?.OrderItemId == newOrder.OrderItemId);
        DataSource.OrderItemList[index] = newOrder;
    }
    /// <summary>
    ///add all list to a new list of order item and return
    /// </summary>
    /// <returns>newList</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        List<OrderItem?> newList=new List<OrderItem?>();
        DataSource.OrderItemList.ForEach(x=> newList.Add(x));
        return newList;
    }
    /// <summary>
    /// find the order with the given value and return it
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>itemInOrder</returns>
    public List<OrderItem?> GetItemsInOrder(int orderId)
    {
        List<OrderItem?> itemsInOrder = new List<OrderItem?>();
       itemsInOrder= DataSource.OrderItemList.FindAll(x => x?.OrderId == orderId);
        return itemsInOrder;
    }
    /// <summary>
    /// find an order item with the given order id and product id and return this object
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns>orderItem</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetItemByOrderAndProduct(int orderId,int productId)
    {
        return DataSource.OrderItemList.FirstOrDefault(Item => (Item.Value.OrderId == orderId)&&(Item.Value.ProductId==productId)) ?? throw new DalIdDoNotExistException(orderId, "orderItem");

    }
}

