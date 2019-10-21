using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class OrderDetailsViewModel
    {
        [Display(Name = "Product ID")]
        public int ProdId { get; set; }

        [Display(Name = "Product")]
        public string ProdName { get; set; }

        [Display(Name = "Description")]
        public string ProdDesc { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
    }
}
