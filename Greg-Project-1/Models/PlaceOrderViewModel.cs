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
        /*
            //customer
            public int CustId { get; set; }
            public dom.Customer Cust { get; set; }

            //location
            public int LocId { get; set; }
            public dom.Location Loc { get; set; }

            //order
            public int OrdId { get; set; }
            public dom.Order Ord { get; set; }
        */
        public int CustId { get; set; }
        public int LocId { get; set; }

        [Display(Name = "Customer")]
        public string CustName { get; set; }

        [Display(Name = "Customer Address")]
        public string CustAddr { get; set; }

        [Display(Name = "Location")]
        public string LocName { get; set; }
        
        [Display(Name = "Location Address")]
        public string LocAddr { get; set; }
    }
}
