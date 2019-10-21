using System;
using System.Collections.Generic;
using System.Text;
using dom = Domains.Library;


namespace Domains.Library.Interfaces
{
    public interface IOrderRepo
    {
        /// <summary>
        /// Maps and adds a Domain Order to the database.
        /// NOTE: This doesn't map or app anything from Order.basket to the database. To do so, use AddBasket().
        /// </summary>
        /// <param name="ordDom">A domain order</param>
        public void AddOrder(dom.Order ordDom);

        /// <summary>
        /// Map and add the items in a Domain Order's basket to the database.
        /// </summary>
        /// <param name="dbId">The ID of the Domain Order's Entity Receipt counterpart in the database.</param>
        public void AddBasket(dom.Order ordDom, int orderId);


        /// <summary>
        /// Get a list of Domain Orders
        /// </summary>
        /// <returns>A list of Domain Orders</returns>z
        public IEnumerable<dom.Order> GetOrders();

        /// <summary>
        /// Get a list of Domain Orders, filtered by orderId
        /// </summary>
        /// <param name="ordId">An Order Id</param>
        /// <returns>A list of Domain Orders</returns>
        public IEnumerable<dom.Order> GetOrderById(int ordId);

        /// <summary>
        /// Get a list of Domain Orders, filtered by a customerId
        /// </summary>
        /// <param name="custId">A Customer Id</param>
        /// <returns>A list of Domain Customers</returns>
        public IEnumerable<dom.Order> GetOrdersByCustomer(int custId);

        /// <summary>
        /// Get a list of Domain Orders, filtered by a locationId
        /// </summary>
        /// <param name="custId">A Location Id</param>
        /// <returns>A list of Domain Customers</returns>
        public IEnumerable<dom.Order> GetOrdersByLocation(int locId);

        /// <summary>
        /// Commits and saves changes to the Database
        /// </summary>
        public void Save();
    }
}
