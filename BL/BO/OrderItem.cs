using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// Unique identification number of each item in all orders
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of this product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// the identification number of this product
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// The price of this product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The quantity of this product in the order
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// The final,total price of this item/s  depends on the amount 
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// override of ToString-returns one string to describe item in order
        /// </summary>
        /// <returns></returns>
        public string? ImagesSource { get; set; }
        public override string ToString() => this.ToStringProperty();



    }
}
