using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Interfaces 
{

    /// <summary>
    /// This is the Interface for the UsersEvent Repository.
    /// It contains the methods that the Repo must
    /// implement, which are based on CRUD operations.
    /// Each main class of the programm will have its own
    /// interface and respective repo class.
    /// These will implement the IDisposable interface
    /// since using database context is unmanaged.
    /// /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public interface IUsersEventRepository : IDisposable
    {

        /// <summary>
        /// Create UsersEvent
        /// </summary>
        void InsertUsersEvent(UsersEvent usersEvent);

        /// <summary>
        /// Read all UsersEvents
        /// </summary>
        List<UsersEvent> GetAllUsersEvents();

        /// <summary>
        /// Read an entity by their UsersEventId
        /// </summary>
        UsersEvent GetUsersEventById(Guid userId, Guid eventId);

        /// <summary>
        /// Update a UserEvent
        /// </summary>
        void UpdateUsersEvent(UsersEvent usersEvent);

        /// <summary>
        /// Delete a UserEvent
        /// </summary>
        void DeleteUsersEvent(Guid userId, Guid eventId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}