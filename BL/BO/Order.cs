using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<OrderItem>? Items { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        order ID:{Id}
        customer name:{CustomerName}
        customer email:{CustomerEmail}
        customer address:{CustomerAddress} 
        order status:{Status}
        date order:{OrderDate}
        ship date:{ShipDate}
        date delivery: {DeliveryDate}
        items in order:{ string.Join(' ',Items)}
        total price for order:{TotalPrice}
";
    }
}
