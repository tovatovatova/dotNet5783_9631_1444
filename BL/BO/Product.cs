using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        /// <summary>
        /// Unique identification number of product
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// the name of the product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// product price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The category of the product
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// int variable describes the amount of product is stock
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        ///  Override function of the function ToString in object class-return string of produc details
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();


    }
}
