using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrder : IOrder
{
    readonly string s_orders="orders";


    public int Add(Order item)
    {
        List<Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
       // newOrder.OrderId = DataSource.Config.NextOrderNumber;
        lstOrd.Add(item);
        XMLTools.SaveListToXMLSerializer<DO.Order>(lstOrd, s_orders);
        return item.OrderId;
    }

    public void Delete(int id)
    {
        List<Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Order? o = lstOrd.FirstOrDefault(order => order?.OrderId == id) ?? throw new DalIdDoNotExistException(id, "order");
        int orderIndex = lstOrd.FindIndex(order => order?.OrderId == id);
        lstOrd.RemoveAt(orderIndex);
        XMLTools.SaveListToXMLSerializer<DO.Order>(lstOrd, s_orders);

    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        List<Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (filter != null)
            return lstOrd.Where(item => filter(item));
        return lstOrd.Select(item => item);
    }

    public Order GetById(int id)
    {
        List<Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return lstOrd.FirstOrDefault(order => order?.OrderId == id) ?? throw new DalIdDoNotExistException(id, "order");
    }

    public void Update(Order item)
    {
        List<Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Order? delOrder = lstOrd.FirstOrDefault(order => order?.OrderId == item.OrderId) ?? throw new DalIdDoNotExistException(item.OrderId, "order");
        int index = lstOrd.FindIndex(x => x?.OrderId == item.OrderId);
        lstOrd[index] = item;
        XMLTools.SaveListToXMLSerializer<DO.Order>(lstOrd, s_orders);

    }
}
