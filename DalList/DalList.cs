using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace Dal
{
 sealed internal class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        private DalList() { }
        public IOrder Order { get; } = new DalOrder();
        public IProduct Product { get; } = new DalProduct();
        public IOrderItem OrderItem { get; } = new DalOrderItem();
    }
}
