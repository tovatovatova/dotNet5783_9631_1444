using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation
{
    internal class Order : IOrder
    {
        public BO.Order GetOrderByID(int orderID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderForList?> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public OrderTracking OrderTracking(int orderID)
        {
            throw new NotImplementedException();
        }

        public BO.Order UpdateDelivery(int orderID)
        {
            throw new NotImplementedException();
        }

        public BO.Order UpdateOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public BO.Order UpdateShip(int orderID)
        {
            throw new NotImplementedException();
        }
    }
}
