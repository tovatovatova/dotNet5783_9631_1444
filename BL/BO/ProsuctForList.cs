using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProsuctForList
    {
        /// <summary>
     /// identification number of product
     /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// name of product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// price of product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// the categoty of product
        /// </summary>
        public Category Category { get; set; }
    /// <summary>
    ///    Override function of the function ToString in object class-return string of produc item details
    /// </summary>
    /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

    }
}
