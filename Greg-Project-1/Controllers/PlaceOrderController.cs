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
    /// <summary>
    /// Controller for placing orders
    /// </summary>
    public class PlaceOrderController : Controller
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
        /// Constructor for Controller
        /// </summary>
        /// <param name="custcontext">Customer DB Context</param>
        /// <param name="loccontext">Location DB Context</param>
        /// <param name="ordcontext">Order DB Context</param>
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

        /// <summary>
        /// The first view of the Create Order
        /// </summary>
        /// <returns>The first view of the Create Order</returns>
        public ActionResult Create()
        {
            ViewData["minCust"] = _custContext.GetCustomers().Select(c => c.CustID).Min();
            ViewData["maxCust"] = _custContext.GetCustomers().Select(c => c.CustID).Max();
            ViewData["minLoc"] = _locContext.GetLocations().Select(l => l.StoreID).Min();
            ViewData["maxLoc"] = _locContext.GetLocations().Select(l => l.StoreID).Max();

            return View();
        }

        /// <summary>
        /// Validates the inputs from Create() and prepares data for the PreDetails Action
        /// </summary>
        /// <param name="collection">The inputs from Create()</param>
        /// <returns>The PreDetails Action</returns>
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

        /// <summary>
        /// Prepares and displays the information for the future order.
        /// </summary>
        /// <returns>A View that allows the Customer to continue or not</returns>
        public ActionResult PreDetails()
        {
            var custId = Convert.ToInt32(TempData["custId"]);
            var cust = _custContext.GetCustomers(custId: custId).FirstOrDefault();
            var locId = Convert.ToInt32(TempData["locId"]);
            var loc = _locContext.GetLocations(locId: locId).FirstOrDefault();

            var orderVM = new Models.PlaceOrderViewModel
            {
                CustId = custId,
                LocId = locId,
                CustName = cust.FullName,
                CustAddr = cust.Address,
                LocName = loc.StoreName,
                LocAddr = loc.Address
            };
            

            TempData.Keep();
            return View(orderVM);
        }

        /// <summary>
        /// Creates a view with the Inventory of the store to purchase from.
        /// </summary>
        /// <returns>a view with the Inventory</returns>
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

        /// <summary>
        /// Parses the form data from Edit() to prepare for Order Placement in PostDetails
        /// </summary>
        /// <param name="collection">The form data from Edit()</param>
        /// <returns>Redirects to Post Details</returns>
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

        /// <summary>
        /// Places the orders
        /// </summary>
        /// <param name="collection">The items in the basket</param>
        /// <returns>The order details</returns>
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

                ord = _ordContext.GetOrderById(ord.OrderId).First();

                return RedirectToAction("Details", "ViewOrder", new { id = ordId});
            }
            catch
            {
                return Redirect(nameof(Index));
            }
        }
    }
}