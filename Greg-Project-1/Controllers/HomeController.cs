using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Greg_Project_1.Models;

namespace Greg_Project_1.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// A Logger
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor for the HomeController
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The main page of the website
        /// </summary>
        /// <returns>The main view of the website</returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Returns a View for any errors that occur
        /// </summary>
        /// <returns>A View for any errors that occur</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
