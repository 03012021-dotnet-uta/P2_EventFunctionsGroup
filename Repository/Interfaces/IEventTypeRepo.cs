using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Interfaces 
{

    /// <summary>
    /// This is the Interface for the EventType Repository.
    /// It contains the methods that the Repo must
    /// implement, which are based on CRUD operations.
    /// Each main class of the programm will have its own
    /// interface and respective repo class.
    /// These will implement the IDisposable interface
    /// since using database context is unmanaged.
    /// /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public interface IEventTypeRepository : IDisposable
    {

        /// <summary>
        /// Insert a new item to context
        /// </summary>
        void InsertEventType(EventType eventType);

        /// <summary>
        /// Get the EventTypes from database and present back to context
        /// </summary>
        List<EventType> GetAllEventTypes();

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        void UpdateEventType(EventType eventType);

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        void DeleteEventType(int eventTypeId);

        /// <summary>
        /// Save changes back to database
        /// </summary>
        void Save();

        /// <summary>
        /// Async gets an eventtype by ID
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<EventType> GetEventTypeByIDAsync(Guid eventType);

    }
}