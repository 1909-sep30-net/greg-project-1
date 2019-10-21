using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class RecieptViewModel
    {
        
        public string Order { get; set; }
        public string Basket { get; set; }

        public int OrderId { get; set; }

        public int OrderCustId { get; set; }
        public string OrderCustName { get; set; }
        public string OrderCustAddress { get; set; }

        public int OrderLocationId { get; set; }
        public string OrderLocationName { get; set; }
        public string OrderLocationAddress { get; set; }
    }
}
