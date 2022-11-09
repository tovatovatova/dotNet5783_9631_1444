

using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.OrderItemId = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemList.Add(newOrderItem);
        return newOrderItem.OrderItemId;
    }
    public OrderItem ReadById(int orderItemId)
    {
        OrderItem? newOrder = DataSource.OrderItemList.Find(x => x.Value.OrderItemId == orderItemId);
        if (newOrder != null)
        {
            return (OrderItem)newOrder;

        }
        throw new Exception("order item not exist");
    }
    public IEnumerable<OrderItem?> GetAll() => DataSource.OrderItemList;
    public void Delete(int orderItemId)
    {
        OrderItem? newOrder = DataSource.OrderItemList.Find(x => x.Value.OrderId == orderItemId);
        if (newOrder == null)
            throw new Exception("order item not exist");
        DataSource.OrderItemList.Remove(newOrder);
    }
    public void Update(OrderItem newOrder)
    {
        int index = DataSource.OrderItemList.FindIndex(x => x.Value.OrderItemId == newOrder.OrderItemId);
        if (index == -1)
            throw new Exception("order itemnot exist");
        DataSource.OrderItemList[index] = newOrder;
    }
}

