using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
     readonly string s_orderItems = "orderItems";

    static DO.OrderItem createOrderItemFromXElement(XElement item)
    {
        return new DO.OrderItem()
        {
            OrderItemId = item.ToIntNullable("OrderItemId") ?? throw new FormatException("OrderItemId"),
            ProductId = item.ToIntNullable("ProductId") ?? throw new FormatException("ProductId"),
            OrderId = item.ToIntNullable("OrderId") ?? throw new FormatException("OrderId"),
            Price = item.ToDoubleNullable("Price") ?? throw new FormatException("Price"),
            Amount = item.ToIntNullable("Amount") ?? throw new FormatException("Amount"),
            ImagesSource=item.Element("ImagesSource").ToString()??throw new FormatException("ImagesSource")
        };
    }


    public int Add(OrderItem item)
    {
        XElement elementItem = XMLTools.LoadListFromXMLElement(s_orderItems);
        item.OrderItemId = Config.NextOrderItemNumber();
        item.ImagesSource= "\\" + item.ImagesSource;
        XElement xOrderItem = new XElement("OrderItem",
                                        new XElement("OrderItemId", item.OrderItemId),
                                        new XElement("ProductId", item.ProductId),
                                        new XElement("OrderId", item.OrderId),
                                        new XElement("Price", item.Price),
                                        new XElement("Amount", item.Amount),
                                        new XElement("ImagesSource",item.ImagesSource));
        elementItem.Add(xOrderItem);
        XMLTools.SaveListToXMLElement(elementItem, s_orderItems);
        Config.SaveNextOrderItemNumber(item.OrderItemId+1);
        return item.OrderItemId;
    }

    public void Delete(int id)
    {
        XElement elementItem = XMLTools.LoadListFromXMLElement(s_orderItems);
        XElement? oItem = (from o in elementItem.Elements()
                           where (o.ToIntNullable("OrderItemId") == id)
                           select o).FirstOrDefault();
        if (oItem == null)//doesnt exist
            throw new DO.DalIdDoNotExistException(id, "order item");
        oItem.Remove();
        XMLTools.SaveListToXMLElement(elementItem, s_orderItems);
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        XElement? orderItemXElent=XMLTools.LoadListFromXMLElement(s_orderItems);
        if(filter != null)
        {
            return from o in orderItemXElent.Elements()
                   let item = createOrderItemFromXElement(o)
                   where filter(item)
                   select (DO.OrderItem?)(item);
        }
        return from o in orderItemXElent.Elements()
               select (DO.OrderItem?)(createOrderItemFromXElement(o));
    }

    public OrderItem GetById(int id)
    {
        XElement? orderItemXElent = XMLTools.LoadListFromXMLElement(s_orderItems);
        XElement? oItem = (from o in orderItemXElent.Elements()
                           where (o.ToIntNullable("OrderItemId") == id)
                           select o).FirstOrDefault();
        if (oItem == null)//doesnt exist
            throw new DO.DalIdDoNotExistException(id, "order item");
        //else-exist
        return createOrderItemFromXElement(oItem);
    }


    public OrderItem GetItemByOrderAndProduct(int orderId, int productId)
    {
        XElement? orderItemXElent = XMLTools.LoadListFromXMLElement(s_orderItems);
        XElement? oItem = (from o in orderItemXElent.Elements()
                           where ((o.ToIntNullable("OrderId") == orderId)&&o.ToIntNullable("ProductId")==productId)
                           select o).FirstOrDefault();
        if (oItem == null)//doesnt exist
            throw new DO.DalIdDoNotExistException(orderId, "order item");
        //else-exist
        return createOrderItemFromXElement(oItem);
    }

    public List<OrderItem?> GetItemsInOrder(int orderId)
    {
        XElement? orderItemXElent = XMLTools.LoadListFromXMLElement(s_orderItems);
        List<OrderItem?> itemsInOrder = (from o in orderItemXElent.Elements()
                                         where (o.ToIntNullable("OrderId") == orderId)
                                         select ((DO.OrderItem?)(createOrderItemFromXElement(o)))).ToList();
        return itemsInOrder;
    }

    public void Update(OrderItem item)
    {
        Delete(item.OrderId);
        Add(item);
    }
}
