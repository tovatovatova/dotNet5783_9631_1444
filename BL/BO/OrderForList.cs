using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        /// <summary>
        /// Unique identification number of each order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of customer that order this order
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The status of the order-update during the Shipping progress
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Amount of Items in the order
        /// </summary>
        public int AmountOfItems { get; set; }
        /// <summary>
        /// The total price of all items together
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Override function of the function ToString in object class-return string of orderForList details
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
    }
}
