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
    public class PlaceOrderController : Controller
    {
        public dom.Interfaces.ICustomerRepo _custContext { get; }
        public dom.Interfaces.ILocationRepo _locContext { get; }
        public dom.Interfaces.IOrderRepo _ordContext { get; }

        public PlaceOrderController
            (
             dom.Interfaces.ICustomerRepo custcontext,
             dom.Interfaces.ILocationRepo loccontext,
             dom.Interfaces.IOrderRepo ordcontext
            )
        {
            _custContext = custcontext ?? throw new ArgumentNullException(nameof(_custContext));
            _locContext = loccontext ?? throw new ArgumentNullException(nameof(_locContext));
            _ordContext = ordcontext ?? throw new ArgumentNullException(nameof(_ordContext));
        }


        // GET: PlaceOrder
        public ActionResult Index()
        {
            return View();
        }

        

        // GET: PlaceOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaceOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                int custId = Convert.ToInt32(collection["custid"]);
                var cust = _custContext.GetCustomers(custId: custId).FirstOrDefault();
                if (cust == null) { return View(); }

                int locId = Convert.ToInt32(collection["locid"]);
                var loc = _locContext.GetLocations(locId).FirstOrDefault();
                if(loc == null) { return View(); }

                TempData["custId"] = custId;
                TempData["locId"] = locId;
                return RedirectToAction(nameof(PreDetails));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaceOrder/Details/5
        public ActionResult PreDetails()
        {
            var custId = Convert.ToInt32(TempData["custId"]);
            var cust = _custContext.GetCustomers(custId: custId).FirstOrDefault().ToString();
            var locId = Convert.ToInt32(TempData["locId"]);
            var loc = _custContext.GetCustomers(custId: custId).FirstOrDefault().ToString();

            var orderVM = new Models.PlaceOrderViewModel
            {
                CustId = custId,
                LocId = locId,
                Cust = cust,
                Loc = loc
            };
            

            TempData.Keep();
            return View(orderVM);
        }

        // GET: PlaceOrder/Edit/5
        public ActionResult Edit()
        {
            var custId = Convert.ToInt32(TempData["custId"]);
            var cust = _custContext.GetCustomers(custId: custId).FirstOrDefault();
            var locId = Convert.ToInt32(TempData["locId"]);
            var loc = _locContext.GetLocations(locId: locId).FirstOrDefault();

            var inventoryVM = loc.Inventory.Select(i => new Models.InventoryItemViewModel
            {
                ProdId = i.Key.ProductID,
                ProdDesc = i.Key.ProductDescription,
                ProdName = i.Key.ProductName,
                ProdToString = i.Key.ToString(),
                Quantity = i.Value
            });


            TempData.Keep();
            return View(inventoryVM);
        }

        // POST: PlaceOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
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

        // GET: PlaceOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlaceOrder/Delete/5
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