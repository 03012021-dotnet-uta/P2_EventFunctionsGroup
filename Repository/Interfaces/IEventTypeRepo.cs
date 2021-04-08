using System;
using System.Collections.Generic;
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
    /// </summary>
    public interface IEventTypeRepository : IDisposable
    {

        /// <summary>
        /// Create EventType
        /// </summary>
        void InsertEventType(EventType eventType);

        /// <summary>
        /// Read all EventTypes
        /// </summary>
        ICollection<EventType> GetAllEventTypes();

        /// <summary>
        /// Update an EventType
        /// </summary>
        void UpdateEventType(EventType eventType);

        /// <summary>
        /// Delete an EventType
        /// </summary>
        void DeleteEventType(int eventTypeId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}