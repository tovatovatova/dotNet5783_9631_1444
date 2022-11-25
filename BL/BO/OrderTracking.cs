using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// Order identification number 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// status of the order- updated during the Shipping progress
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// the tracking details- stage in Shipping progress and the date of it
        /// </summary>
        public List<Tuple<DateTime?, string>>? Tracking { get; set; }
        /// <summary>
        /// override of ToString-returns one string to describe order tracking
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();


    }
}
