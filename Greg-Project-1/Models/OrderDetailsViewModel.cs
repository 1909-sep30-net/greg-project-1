using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class OrderDetailsViewModel
    {
        /// <summary>
        /// The Product Id
        /// </summary>
        [Display(Name = "Product ID")]
        public int ProdId { get; set; }

        /// <summary>
        /// The Product Name
        /// </summary>
        [Display(Name = "Product")]
        public string ProdName { get; set; }

        /// <summary>
        /// The Product Description
        /// </summary>
        [Display(Name = "Description")]
        public string ProdDesc { get; set; }

        /// <summary>
        /// The Quantity of that product on the reciept
        /// </summary>
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// The cost per unit of the product
        /// </summary>
        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
    }
}
