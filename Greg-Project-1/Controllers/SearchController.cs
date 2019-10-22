using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greg_Project_1.Controllers
{
    public class SearchController : Controller
    {
        /// <summary>
        /// Returns the view with the Search menu
        /// </summary>
        /// <returns>The search menu view</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}