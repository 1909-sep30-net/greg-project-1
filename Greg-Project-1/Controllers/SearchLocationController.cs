using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dom = Domains.Library;
using dat = Data.Library; 

namespace Greg_Project_1.Controllers
{
    public class SearchLocationController : Controller
    {
        /// <summary>
        /// The DB context with location functions
        /// </summary>
        public dom.Interfaces.ILocationRepo _locContext { get; }

        /// <summary>
        /// The Construct for SearchLocation
        /// </summary>
        /// <param name="context"></param>
        public SearchLocationController(dom.Interfaces.ILocationRepo context) =>
            _locContext = context ?? throw new ArgumentNullException(nameof(_locContext));


        /// <summary>
        /// The View with All Locations in the DB
        /// </summary>
        /// <returns>A View with all Locations</returns>
        public ActionResult Index()
        {
            var locations = _locContext.GetLocations().ToList();
            var viewmodel = locations.Select(l => new Models.SearchLocationViewModel
            {
                Id = l.StoreID,
                Name = l.StoreName,
                Address = l.Address
            }); 

            return View(viewmodel);
        }
    }
}