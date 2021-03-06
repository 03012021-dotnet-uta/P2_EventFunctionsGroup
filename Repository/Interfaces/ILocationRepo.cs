using System;
using System.Collections.Generic;
using Domain.Models;
   
namespace Repository.Interfaces 
{

    /// <summary>
    /// This is the Interface for the Location Repository.
    /// It contains the methods that the Repo must
    /// implement, which are based on CRUD operations.
    /// Each main class of the programm will have its own
    /// interface and respective repo class.
    /// These will implement the IDisposable interface
    /// since using database context is unmanaged.
    /// /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public interface ILocationRepository : IDisposable
    {

        /// <summary>
        /// Create Location
        /// </summary>
        void InsertLocation(Location location);

        /// <summary>
        /// Read all locations
        /// </summary>
        ICollection<Location> GetAllLocations();

        /// <summary>
        /// Read an entity by their LocationId
        /// </summary>
        Location GetLocationById(Guid locationId);

        /// <summary>
        /// Update a location
        /// </summary>
        void UpdateLocation(Location location);

        /// <summary>
        /// Gets a location based off long/lat
        /// </summary>
        /// <param name="longtitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        Location GetLocationByCoord(double longtitude, double latitude);

        /// <summary>
        /// Delete a location
        /// </summary>
        void DeleteLocation(Guid locationId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}