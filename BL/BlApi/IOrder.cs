

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
       public BO.Order GetOrderByID(int orderID); //manager and client
        public IEnumerable<BO.OrderForList?> GetOrderList();//manager
        public BO.Order UpdateShip(int orderID);//manager
        public BO.Order UpdateDelivery(int orderID);//manager
       public BO.OrderTracking OrderTracking (int orderID);//manager
        public IEnumerable<BO.OrderForList?> GetListedListByFilter(Func<BO.OrderForList?, bool>? filter = null);



    }
}
