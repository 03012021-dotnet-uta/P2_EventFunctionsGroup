using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Interfaces 
{
 
    /// <summary>
    /// This is the Interface for the User Repository.
    /// It contains the methods that the Repo must
    /// implement, which are based on CRUD operations.
    /// Each main class of the programm will have its own
    /// interface and respective repo class.
    /// These will implement the IDisposable interface
    /// since using database context is unmanaged.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public interface IUserRepository : IDisposable
    {

        /// <summary>
        /// Create User
        /// </summary>
        void InsertUser(User user);

        /// <summary>
        /// Read all users
        /// </summary>
        ICollection<User> GetAllUsers();

        /// <summary>
        /// Read an entity by their UserId
        /// </summary>
        User GetUserById(int userId);
        
        /// <summary>
        /// Update a user
        /// </summary>
        void UpdateUser(User user);

        /// <summary>
        /// Delete a user
        /// </summary>
        void DeleteUser(int userId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}