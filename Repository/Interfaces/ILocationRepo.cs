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
        /// Update a location
        /// </summary>
        void UpdateLocation(Location location);

        /// <summary>
        /// Delete a location
        /// </summary>
        void DeleteLocation(int locationId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}