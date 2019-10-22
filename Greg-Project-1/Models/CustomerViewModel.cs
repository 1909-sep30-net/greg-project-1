using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Greg_Project_1.Models
{
    /// <summary>
    /// A View Model that caters to the CustomerController. Imitates Domains.Library.Customer
    /// </summary>
    public class CustomerViewModel
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        [Display(Name = "Customer ID")]
        public int Id { get; set; }

        /// <summary>
        /// Customer First Name
        /// </summary>
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Customer Last Name
        /// </summary>
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }
    }
}
