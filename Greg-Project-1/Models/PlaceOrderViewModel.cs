using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dom = Domains.Library;

namespace Greg_Project_1.Models
{
    public class PlaceOrderViewModel
    {
        /// <summary>
        /// The Customer ID
        /// </summary>
        public int CustId { get; set; }

        /// <summary>
        /// The Location ID
        /// </summary>
        public int LocId { get; set; }

        /// <summary>
        /// The Name of the Customer
        /// </summary>
        [Display(Name = "Customer")]
        public string CustName { get; set; }

        /// <summary>
        /// The Address of the Customer
        /// </summary>
        [Display(Name = "Customer Address")]
        public string CustAddr { get; set; }

        /// <summary>
        /// The Name of the Location
        /// </summary>
        [Display(Name = "Location")]
        public string LocName { get; set; }
        
        /// <summary>
        /// The Address of the Location
        /// </summary>
        [Display(Name = "Location Address")]
        public string LocAddr { get; set; }
    }
}
