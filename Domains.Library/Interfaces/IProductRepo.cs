using System;
using System.Collections.Generic;
using System.Text;
using dom = Domains.Library;


namespace Domains.Library.Interfaces
{
    public interface IProductRepo
    {
        /// <summary>
        /// Gets a list of Domain Products
        /// Can be filtered by ProductId
        /// </summary>
        /// <param name="prodId">A ProductId to filter by</param>
        /// <returns>A List of Domain Products</returns>
        public IEnumerable<dom.Product> GetProducts(int prodId = -1);

        /// <summary>
        /// Commits and saves changes to the Database
        /// </summary>
        public void Save();
    }
}
