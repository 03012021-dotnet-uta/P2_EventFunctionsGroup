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
        /// Insert a new item to context
        /// </summary>
        /// <param name="user">New User</param>
        User InsertUser(User user);

        /// <summary>
        /// Get the Users from database and present back to context
        /// </summary>
        List<User> GetAllUsers();

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        void UpdateUser(User user);

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        void DeleteUser(Guid userId);

        /// <summary>
        /// Save changes back to database
        /// </summary>
        void Save();

        /// <summary>
        /// Gets user by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        User GetUserByID(Guid id);

    }
}