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
        /// <summary>
        ///  Unique identification number of each order
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the customer that make this order
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// Email address of the customer
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// Customer address for delivery
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// The status of the order(Initiated/Ordered/Paid/Shipped/Delivered)
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// The date when the order created
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// The date when the order is ship to the customer
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// The date of the delivery, when the customer get the order
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Collection of items in order
        /// </summary>
        public IEnumerable<OrderItem>? Items { get; set; }
        /// <summary>
        /// The total price of the order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// function to return one string of the order details-ovveride the function Tostring of object class
        /// </summary>
        /// <returns></returns>
        //        public override string ToString() => $@"
        //        order ID:{Id}
        //        customer name:{CustomerName}
        //        customer email:{CustomerEmail}
        //        customer address:{CustomerAddress} 
        //        order status:{Status}
        //        date order:{OrderDate}
        //        ship date:{ShipDate}
        //        date delivery: {DeliveryDate}
        //        items in order:{ string.Join(' ',Items)}
        //        total price for order:{TotalPrice}
        //";
        public override string ToString() => this.ToStringProperty();

    }
}
