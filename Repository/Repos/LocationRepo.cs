using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Interfaces;
using Domain.Models;

namespace Repository.Repos
{
    /// <summary>
    /// This is the Repo class for Locations
    /// It implements the CRUD functions from its
    /// respective interface.
    /// It also implements Disposal pattern since it
    /// contains unmanaged resources.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application</summary>
    public class LocationRepo : ILocationRepository
    {
    
        /// <summary>
        /// Context variable that will be injected and used by business logic
        /// </summary>
        private readonly EventFunctionsContext context;

        /// <summary>
        /// Empty constructor to instantiate the context
        /// and then assign to context variable
        /// </summary>
        public LocationRepo() 
        {
            context = new EventFunctionsContext();
        }

        /// <summary>
        /// Pass in context using Dependency Injection
        /// and assign to context variable
        /// </summary>
        public LocationRepo(EventFunctionsContext eventFunctionsContext) 
        {
            context = eventFunctionsContext;
        }

        /// <summary>
        /// Insert a new item to context
        /// </summary>
        public void InsertLocation(Location location) 
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }

        /// <summary>
        /// Get the Locations from database and present back to context
        /// </summary>
        public ICollection<Location> GetAllLocations() 
        {
            return context.Locations.ToList();
        }

        /// <summary>
        /// Get an entity by its LocationId
        /// </summary>
        public Location GetLocationById(Guid locationId)
        {
            return context.Locations.Find(locationId);
        }

        public Location GetLocationByCoord(double longtitude, double latitude)
        {
            Location findLoc = context.Locations.Where(n => n.Longtitude == longtitude).Where(n => n.Latitude == latitude).FirstOrDefault();

            return findLoc;
        }
        public Location GetLocationByAddress(string street, string v)
        {
            Location findLoc = context.Locations.Where(n => n.Name.ToLower() == street.ToLower()).Where(n => n.Address.ToLower() == v.ToLower()).FirstOrDefault();
        
            return findLoc;
        }

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        public void UpdateLocation(Location location) 
        {
            context.Entry(location).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        public void DeleteLocation(Guid locationId)
        {
            Location location = context.Locations.Find(locationId);
            context.Entry(location).State = EntityState.Deleted;
            context.Locations.Remove(location);
            
        }

        /// <summary>
        /// Save changes back to database
        /// </summary>
        public void Save() 
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Implementing the disposal pattern since
        /// repo/context is unmanaged resource
        /// Interface requires Dispose() and Dipose(bool)
        /// Dispose(bool) to determine if call comes
        /// from a Dispose method or from a finalizer
        /// ref:
        /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        /// Dispose() to inform GC that finalizer does not have to run
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                context.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>
        /// Standard implementation to free the actual memory
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // prevent Garbage Collector from running finalizer
            GC.SuppressFinalize(this);
        }


    }
}