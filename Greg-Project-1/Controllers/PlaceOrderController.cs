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
            var loc = _locContext.GetLocations(locId: locId).FirstOrDefault().ToString();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            var coll = new Dictionary<int, int> { };//prodId, Quantity to buy

            foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> collItem in collection)
            {
                try
                {
                    var prodId = Convert.ToInt32(collItem.Key);
                    var quantity = Convert.ToInt32(Convert.ToString(collItem.Value));
                    coll.Add(prodId, quantity);
                }
                catch { }
            }

            TempData["coll"] = coll;
            TempData.Keep();

            return Redirect(nameof(PostDetails));
        }


        // POST: PlaceOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostDetails(IFormCollection collection)
        {
            try
            {
                var custId = Convert.ToInt32(TempData["custId"]);
                var cust = _custContext.GetCustomers(custId: custId).FirstOrDefault();
                var locId = Convert.ToInt32(TempData["locId"]);
                var loc = _locContext.GetLocations(locId: locId).FirstOrDefault();
                var ord = new dom.Order(cust, loc);


                var coll = new Dictionary<int, int> { };//prodId, Quantity to buy

                foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> collItem in collection)
                {
                    try
                    {
                        var prodId = Convert.ToInt32(collItem.Key);
                        var quantity = Convert.ToInt32(Convert.ToString(collItem.Value));
                        coll.Add(prodId, quantity);
                    }
                    catch { }
                }

                foreach (KeyValuePair<dom.Product, int> item in loc.Inventory) //prodId, amount in stock
                {
                    foreach(KeyValuePair<int, int> collItem in coll)
                    {
                        if(item.Key.ProductID == collItem.Key)
                        {
                            ord.basket.Add(item.Key, collItem.Value);//prod, quantity to buy
                            
                        }
                    }
                }

                foreach (KeyValuePair<dom.Product, int> item in ord.basket)
                {
                    loc.AdjustQuantity(item.Key, -1 * item.Value);
                }

                _locContext.UpdateInventory(loc);
                _locContext.Save();

                _ordContext.AddOrder(ord);
                _ordContext.Save();
                var ordId = _ordContext.GetOrdersByCustomer(ord.OrderCustomer.CustID).Last().OrderId;
                ord.OrderId = ordId;
                TempData["ordId"] = ord.OrderId;
                _ordContext.AddBasket(ord, ordId);
                _ordContext.Save();


                var reciept = new Models.RecieptViewModel
                {
                    Order = ord.ToString(),
                    Basket = ord.BasketToString(),
                };

                return View(reciept);
            }
            catch
            {
                return Redirect(nameof(Index));
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