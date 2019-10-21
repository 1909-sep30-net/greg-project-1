using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Greg_Project_1.Models
{
    public class InventoryItemViewModel
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public int Quantity { get; set; }
        public string ProdToString { get; set; }
    }
}
