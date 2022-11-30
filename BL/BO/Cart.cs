using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Cart
    {
        /// <summary>
        /// Name of the customer
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// Email address of the customer
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// Address of the customer for delivery
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// Collection for the items in a specific order
        /// </summary>
        public IEnumerable<OrderItem>? Items { get; set; }
        /// <summary>
        /// Total price of the shopping cart now
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// function return string describing cart object properties and values
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

        //        public override string ToString() => @$"

        //        customer name:{CustomerName}
        //        customer email:{CustomerEmail}
        //        customer address:{CustomerAddress} 
        //        items in cart:{string.Join("", Items)}
        //        total price:{TotalPrice}
        //";

        //    }
    }
}
