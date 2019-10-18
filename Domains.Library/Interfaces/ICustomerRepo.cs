using System;
using System.Collections.Generic;
using System.Text;
using dom = Domains.Library;

namespace Domains.Library.Interfaces
{
    public interface ICustomerRepo
    {
        
        /// <summary>
        /// Gets a list of Domain Customers.
        /// Can be filtered by First and/or Last name.
        /// </summary>
        /// <param name="firstName">The First name to filter by</param>
        /// <param name="lastName">The Last name to filter by</param>
        /// <returns>A list of Domain Customers</returns>
        public IEnumerable<dom.Customer> GetCustomers(string firstName = null, string lastName = null, int custId = -1);

        /// <summary>
        /// Maps and adds a Domain Customer to an Entity Customer in the Database
        /// </summary>
        /// <param name="custDom">A Domain Customer</param>
        public void AddCustomer(dom.Customer custDom);


        /// <summary>
        /// Commits and saves changes to the Database
        /// </summary>
        public void Save();
    }
}
