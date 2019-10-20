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
    public class CustomerController : Controller
    {
        public dom.Interfaces.ICustomerRepo _custContext { get; }

        public CustomerController(dom.Interfaces.ICustomerRepo context) =>
            _custContext = context ?? throw new ArgumentNullException(nameof(_custContext));




        // GET: Customer
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



        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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