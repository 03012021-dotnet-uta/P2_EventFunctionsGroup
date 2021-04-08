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
        ICollection<UsersEvent> GetAllUsersEvents();

        /// <summary>
        /// Update a UserEvent
        /// </summary>
        void UpdateUsersEvent(UsersEvent usersEvent);

        /// <summary>
        /// Delete a UserEvent
        /// </summary>
        void DeleteUsersEvent(int userEventId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}