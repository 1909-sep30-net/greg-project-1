using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class OrderViewModel
    {
        /// <summary>
        /// The OrderId
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The Customer Id
        /// </summary>
        public int CustId { get; set; }
        
        /// <summary>
        /// The Customer Name
        /// </summary>
        public string CustName { get; set; }

        /// <summary>
        /// The Location Id
        /// </summary>
        public int LocId { get; set; }

        /// <summary>
        /// The Location Name
        /// </summary>
        public string LocName { get; set; }

        /// <summary>
        /// The order Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The Total Cost of the order
        /// </summary>
        public decimal TotalCost { get; set; }

    }
}
