using System;
using System.Collections.Generic;
using System.Text;
using dom = Domains.Library;


namespace Domains.Library.Interfaces
{
    public interface ILocationRepo
    {
        /// <summary>
        /// Gets a list of Domain Locations
        /// Can be filtered by LocationId
        /// </summary>
        /// <param name="locId">A LocationId to filter by</param>
        /// <returns>A List of Domain Locations</returns>
        public IEnumerable<dom.Location> GetLocations(int locId = -1);

        /// <summary>
        /// Updates the Database Inventory with changes made to it's parallel Domain
        /// </summary>
        /// <param name="locDom">The domain Location with the inventory which will update the database</param>
        public void UpdateInventory(dom.Location locDom);


        /// <summary>
        /// Commits and saves changes to the Database
        /// </summary>
        public void Save();
    }
}
