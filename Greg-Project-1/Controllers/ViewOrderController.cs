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
    /// The Controller for ViewOrder
    /// </summary>
    public class ViewOrderController : Controller
    {
        /// <summary>
        /// A DBContext with functions for orders
        /// </summary>
        public dom.Interfaces.IOrderRepo _ordContext { get; }

        /// <summary>
        /// Constructor for ViewOrder
        /// </summary>
        /// <param name="context">A DBContext with functions for orders</param>
        public ViewOrderController(dom.Interfaces.IOrderRepo context) =>
            _ordContext = context ?? throw new ArgumentNullException(nameof(_ordContext));




        /// <summary>
        /// Makes a View with a list of orders
        /// </summary>
        /// <param name="custId">A customer Id to search by</param>
        /// <param name="locId">A location id to search by</param>
        /// <returns>a View with a list of orders</returns>
        public ActionResult Index([FromQuery]int custId = -1, [FromQuery]int locId = -1)
        {
            IEnumerable<dom.Order> ordDom;
            if (custId != -1)
            {
                ordDom = _ordContext.GetOrdersByCustomer(custId);
            }
            else if (locId != -1)
            {
                ordDom = _ordContext.GetOrdersByLocation(locId);
            }
            else
            {
                ordDom = _ordContext.GetOrders();
            }
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

        /// <summary>
        /// The Details of a single order
        /// </summary>
        /// <param name="id">The Order ID</param>
        /// <returns>A View with comprhensive details of a single order</returns>
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
    }
}