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
        Event InsertEvent(Event eventName);

        /// <summary>
        /// Get the Events from database and present back to context
        /// </summary>
        List<Event> GetAllEvents();

        /// <summary>
        /// Read an entity by their EventId
        /// </summary>
        Event GetEventById(int eventId);

        /// <summary>
        /// Update an EventRepo
        /// </summary>
        void UpdateEvent(Event eventName);

        /// <summary>
        /// Delete an EventRepo
        /// </summary>
        void DeleteEvent(int eventId);

        /// <summary>
        /// Save changes back to database
        /// </summary>
        void Save();

        /// <summary>
        /// Gets event details with EventId
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns></returns>
        Event GetEventByID(Guid id);

        /// <summary>
        /// Gets all events made by a manager using managerID
        /// </summary>
        /// <param name="id">Manager id</param>
        /// <returns></returns>
        List<Event> GetAllManagerEvents(Guid id);

        /// <summary>
        /// Gets all future events
        /// </summary>
        /// <param name="now">Current Time</param>
        /// <returns></returns>
        List<Event> GetUpcomingEvents(DateTime now);

        /// <summary>
        /// Gets all previous events
        /// </summary>
        /// <param name="now">Current time</param>
        /// <returns></returns>
        List<Event> GetPreviousEvents(DateTime now);

        /// <summary>
        /// Gets all event a user signed up for using userID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        ICollection<Event> GetSignedUpEvents(Guid id);

        /// <summary>
        /// Gets total number of users registered to an event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns></returns>
        int GetTotalAttend(Guid id);

        /// <summary>
        /// Gets a list of all users attending an event by EventID
        /// </summary>
        /// <param name="eid">Event ID</param>
        /// <returns></returns>
        List<User> GetAllAttending(Guid eid);

    }
}