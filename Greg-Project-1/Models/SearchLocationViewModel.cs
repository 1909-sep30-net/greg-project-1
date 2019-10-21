using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    public class SearchLocationViewModel
    {
        [Display(Name = "Location ID")]
        public int Id { get; set; }
        [Display(Name = "Store")]
        public string Name { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
