using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CustId { get; set; }
        public string CustName { get; set; }
        public int LocId { get; set; }
        public string LocName { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal TotalCost { get; set; }

    }
}
