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
    public class ViewOrderController : Controller
    {
        public dom.Interfaces.IOrderRepo _ordContext { get; }

        public ViewOrderController(dom.Interfaces.IOrderRepo context) =>
            _ordContext = context ?? throw new ArgumentNullException(nameof(_ordContext));




        // GET: ViewOrder
        public ActionResult Index()
        {
            var ordDom = _ordContext.GetOrders();
            var ordVM = ordDom.Select(o => new Models.OrderViewModel
            {
                OrderId = o.OrderId,
                CustId = o.OrderCustomer.CustID,
                CustName = o.OrderCustomer.FullName,
                LocId = o.OrderLocation.StoreID,
                LocName = o.OrderLocation.StoreName,
                Timestamp = o.OrderTimestamp,
                TotalCost = Math.Round(o.CalculateCostOfBasket(),2)
            });

            return View(ordVM);
        }

        // GET: ViewOrder/Details/5
        public ActionResult Details(int id)
        {
            var ordDom = _ordContext.GetOrderById(id).First();

            ViewData["OrderId"] = ordDom.OrderId;
            ViewData["CustId"] = ordDom.OrderCustomer.CustID;
            ViewData["CustName"] = ordDom.OrderCustomer.FullName;
            ViewData["CustAddress"] = ordDom.OrderCustomer.Address;
            ViewData["LocId"] = ordDom.OrderLocation.StoreID;
            ViewData["LocName"] = ordDom.OrderLocation.StoreName;
            ViewData["LocAddress"] = ordDom.OrderLocation.Address;
            ViewData["Timestamp"] = ordDom.OrderTimestamp;
            ViewData["TotalCost"] = Math.Round(ordDom.CalculateCostOfBasket(), 2);

            var vM = ordDom.basket.Select(b => new Models.OrderDetailsViewModel
            {
                ProdId = b.Key.ProductID,
                ProdName = b.Key.ProductName,
                ProdDesc = b.Key.ProductDescription,
                UnitCost = Math.Round(b.Key.Cost, 2),
                Quantity = b.Value
            });

            return View(vM);
        }

        // GET: ViewOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewOrder/Create
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

        // GET: ViewOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ViewOrder/Edit/5
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

        // GET: ViewOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewOrder/Delete/5
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