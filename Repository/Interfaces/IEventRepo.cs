using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Interfaces 
{

    /// <summary>
    /// This is the Interface for the Event Repository.
    /// It contains the methods that the Repo must
    /// implement, which are based on CRUD operations.
    /// Each main class of the programm will have its own
    /// interface and respective repo class.
    /// These will implement the IDisposable interface
    /// since using database context is unmanaged.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public interface IEventRepository : IDisposable
    {

        /// <summary>
        /// Create EventRepo
        /// </summary>
        void InsertEvent(Event eventName);

        /// <summary>
        /// Read all EventRepos
        /// </summary>
        ICollection<Event> GetAllEvents();

        /// <summary>
        /// Update an EventRepo
        /// </summary>
        void UpdateEvent(Event eventName);

        /// <summary>
        /// Delete an EventRepo
        /// </summary>
        void DeleteEvent(int eventId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}