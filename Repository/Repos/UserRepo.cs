using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Interfaces;
using Domain.Models;

namespace Repository.Repos
{
    /// <summary>
    /// This is the Repo class for Users
    /// It implements the CRUD functions from its
    /// respective interface.
    /// It also implements Disposal pattern since it
    /// contains unmanaged resources.
    /// </summary>
    public class UserRepo : IUserRepository
    {
    
        /// <summary>
        /// Context variable that will be injected and used by business logic
        /// </summary>
        private readonly EventFunctionsContext context;


        /// <summary>
        /// Empty constructor to instantiate the context
        /// and then assign to context variable
        /// </summary>
        public UserRepo() 
        {
            context = new EventFunctionsContext();
        }

        /// <summary>
        /// Pass in context using Dependency Injection
        /// and assign to context variable
        /// </summary>
        /// <param name="eventFunctionsContext"></param>
        public UserRepo(EventFunctionsContext eventFunctionsContext) 
        {
            context = eventFunctionsContext;
        }

        /// <summary>
        /// Insert a new item to context
        /// </summary>
        /// <param name="user"></param>
        public void InsertUser(User user) 
        {
            context.Users.Add(user);
        }

        /// <summary>
        /// Get the Users from database and present back to context
        /// </summary>
        /// <returns></returns>
        public ICollection<User> GetAllUsers() 
        {
            return context.Users.ToList();
        }

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user) 
        {
            context.Entry(user).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            User user = context.Users.Find(userId);
            context.Entry(user).State = EntityState.Deleted;
            context.Users.Remove(user);
        }

        /// <summary>
        /// Save changes back to database
        /// </summary>
        public void Save() 
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Implementing the disposal pattern since
        /// repo/context is unmanaged resource
        /// Interface requires Dispose() and Dipose(bool)
        /// Dispose(bool) to determine if call comes
        /// from a Dispose method or from a finalizer
        /// ref:
        /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        /// Dispose() to inform GC that finalizer does not have to run
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        /// <summary>
        /// Standard implementation to free the actual memory
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // prevent Garbage Collector from running finalizer
            GC.SuppressFinalize(this);
        }


    }
}