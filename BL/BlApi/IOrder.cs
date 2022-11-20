using BO;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
       public Order GetOrderByID(int orderID); //manager and client
        public IEnumerable<OrderForList> GetOrderList();//manager
        public Order UpdateShip(int orderID);//manager
        public Order UpdateDelivery(int orderID);//manager
       public OrderTracking OrderTracking (int orderID);//manager
        public Order UpdateOrder(int orderId);//manager


        
    }
}
