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
    public class SearchCustomerController : Controller
    {
        public dom.Interfaces.ICustomerRepo _custContext { get; }

        public SearchCustomerController(dom.Interfaces.ICustomerRepo context) =>
            _custContext = context ?? throw new ArgumentNullException(nameof(_custContext));




        // GET: SearchCustomer
        public ActionResult Index([FromQuery]int custid = -1, [FromQuery]string firstname = "", [FromQuery]string lastname = "")
        {
            IEnumerable<dom.Customer> custDom= _custContext.GetCustomers(firstname, lastname, custid);
            IEnumerable<Models.SearchCustomerViewModel> custVM = custDom.Select(c => new Models.SearchCustomerViewModel
            {
                Id = c.CustID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address
            });


            return View(custVM);
        }



        // GET: SearchCustomer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchCustomer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchCustomer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
                return View();
            }
        }

        // GET: SearchCustomer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchCustomer/Edit/5
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

        // GET: SearchCustomer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchCustomer/Delete/5
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