using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dom = Domains.Library;
using dat = Data.Library;
using Microsoft.Extensions.Logging;

namespace Greg_Project_1.Controllers
{
    public class CustomerController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        public dom.Interfaces.ICustomerRepo _custContext { get; }

        public ILogger<CustomerController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public CustomerController
            (dom.Interfaces.ICustomerRepo context,
            ILogger<CustomerController> logger)
        {
            _custContext = context ?? throw new ArgumentNullException(nameof(_custContext));
            _logger = logger;
        }
    


        /// <summary>
        /// Creates a page consisting of a list of customers in the database, with optional filters
        /// </summary>
        /// <param name="custid">Filter for Cust ID</param>
        /// <param name="firstname">Filter for First Name</param>
        /// <param name="lastname">Filter for Last Name</param>
        /// <returns></returns>
        public ActionResult Index([FromQuery]int custid = -1, [FromQuery]string firstname = "", [FromQuery]string lastname = "")
        {
            IEnumerable<dom.Customer> custDom= _custContext.GetCustomers(firstname, lastname, custid);
            IEnumerable<Models.CustomerViewModel> custVM = custDom.Select(c => new Models.CustomerViewModel
            {
                Id = c.CustID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address
            });


            return View(custVM);
        }


        /// <summary>
        /// Returns a view with form to create a new Customer and add it to the DB
        /// </summary>
        /// <returns>A View</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Adds a new customer to the DB with the parameters from the form from Create()
        /// </summary>
        /// <param name="collection">The parameters from the form</param>
        /// <returns>Redirects to Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                this._logger.LogInformation("Attempting to add customer with firstname", collection["firstname"]);
                this._logger.LogInformation("Attempting to add customer with lastname", collection["lastname"]);
                this._logger.LogInformation("Attempting to add customer with address", collection["address"]);

                dom.Customer cust = new dom.Customer(
                    Convert.ToString(collection["firstname"]),
                    Convert.ToString(collection["lastname"]),
                    Convert.ToString(collection["address"]));

                _custContext.AddCustomer(cust);
                _custContext.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                this._logger.LogError("Failure to add customer");
                return View();
            }
        }
    }
}