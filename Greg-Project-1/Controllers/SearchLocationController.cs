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
        public dom.Interfaces.ILocationRepo _locContext { get; }

        public SearchLocationController(dom.Interfaces.ILocationRepo context) =>
            _locContext = context ?? throw new ArgumentNullException(nameof(_locContext));


        // GET: SearchLocation
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

        // GET: SearchLocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchLocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchLocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchLocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchLocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchLocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchLocation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}