using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class SearchLocationViewModel
    {
        /// <summary>
        /// The Location ID
        /// </summary>
        [Display(Name = "Location ID")]
        public int Id { get; set; }

        /// <summary>
        /// The Location Name
        /// </summary>
        [Display(Name = "Store")]
        public string Name { get; set; }

        /// <summary>
        /// The Location Address
        /// </summary>
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
