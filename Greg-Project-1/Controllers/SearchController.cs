using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using dom = Domains.Library;

namespace Greg_Project_1.Controllers
{
    public class SearchController : Controller
    {
        /// <summary>
        /// Db Context with manipulation of customers
        /// </summary>
        public dom.Interfaces.ICustomerRepo _custContext { get; }

        /// <summary>
        /// Db Context with manipulation of locations
        /// </summary>
        public dom.Interfaces.ILocationRepo _locContext { get; }

        /// <summary>
        /// Db Context with manipulation of orders
        /// </summary>
        public dom.Interfaces.IOrderRepo _ordContext { get; }
        /// <summary>
        /// Db Context with manipulation of products
        /// </summary>
        public dom.Interfaces.IProductRepo _prodContext { get; }

        /// <summary>
        /// Constructor for Controller
        /// </summary>
        /// <param name="custcontext">Customer DB Context</param>
        /// <param name="loccontext">Location DB Context</param>
        /// <param name="ordcontext">Order DB Context</param>
        /// <param name="prodcontext">Product DB Context</param>
        public SearchController
            (
             dom.Interfaces.ICustomerRepo custcontext,
             dom.Interfaces.ILocationRepo loccontext,
             dom.Interfaces.IOrderRepo ordcontext,
             dom.Interfaces.IProductRepo prodcontext
            )
        {
            _custContext = custcontext ?? throw new ArgumentNullException(nameof(_custContext));
            _locContext = loccontext ?? throw new ArgumentNullException(nameof(_locContext));
            _ordContext = ordcontext ?? throw new ArgumentNullException(nameof(_ordContext));
            _prodContext = prodcontext ?? throw new ArgumentNullException(nameof(_prodContext));
        }


        /// <summary>
        /// Returns the view with the Search menu
        /// </summary>
        /// <returns>The search menu view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Saves the current state of the database to the disk
        /// </summary>
        /// <returns>Redirects back to index</returns>
        public async Task<ActionResult> Write()
        {
            var customers = _custContext.GetCustomers().ToList();
            await Serialize.JsonToFileAsync(@"C:\revature\greg-project-1\json\custData.json", customers);

            var products = _prodContext.GetProducts().ToList();
            await Serialize.JsonToFileAsync(@"C:\revature\greg-project-1\json\prodData.json", products);

            var locations = _locContext.GetLocations().ToList();
            await Serialize.JsonToFileAsync(@"C:\revature\greg-project-1\json\locData.json", locations);

            var orders = _ordContext.GetOrders().ToList();
            await Serialize.JsonToFileAsync(@"C:\revature\greg-project-1\json\ordData.json", orders);

            return Redirect(nameof(Index));
        }
    }
}