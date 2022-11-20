using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public OrderStatus Status { get; set; }
        public int AmountOfItems { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
order id: {ID}
customer name:{CustomerName}
order status: {Status}
amount of items:{AmountOfItems}
totel price: {TotalPrice}
";

       


    }
}
