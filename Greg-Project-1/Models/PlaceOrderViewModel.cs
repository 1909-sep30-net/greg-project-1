using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dom = Domains.Library;

namespace Greg_Project_1.Models
{
    public class PlaceOrderViewModel
    {
        //customer
        public int CustId { get; set; }
        public dom.Customer Cust { get; set; }

        //location
        public int LocId { get; set; }
        public dom.Location Loc { get; set; }

        //order
        public int OrdId { get; set; }
        public dom.Order Ord { get; set; }
    }
}
