using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Interfaces;
using Domain.Models;
using System.Threading.Tasks;

namespace Repository.Repos
{
    /// <summary>
    /// This is the Repo class for UsersEvent
    /// It implements the CRUD functions from its
    /// respective interface.
    /// It also implements Disposal pattern since it
    /// contains unmanaged resources.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public class UsersEventRepo : IUsersEventRepository
    {
    
        /// <summary>
        /// Context variable that will be injected and used by business logic
        /// </summary>
        private readonly EventFunctionsContext context;


        /// <summary>
        /// Empty constructor to instantiate the context
        /// and then assign to context variable
        /// </summary>
        public UsersEventRepo() 
        {
            context = new EventFunctionsContext();
        }

        /// <summary>
        /// Pass in context using Dependency Injection
        /// and assign to context variable
        /// </summary>
        public UsersEventRepo(EventFunctionsContext eventFunctionsContext) 
        {
            context = eventFunctionsContext;
        }

        /// <summary>
        /// Insert a new item to context
        /// </summary>
        public void InsertUsersEvent(UsersEvent userEvent) 
        {
            context.UsersEvents.Add(userEvent);
            Save();
        }

        /// <summary>
        /// Get the UsersEvents from database and present back to context
        /// </summary>
        public ICollection<UsersEvent> GetAllUsersEvents() 
        {
            return context.UsersEvents.ToList();
        }

        /// <summary>
        /// Get an entity by its UsersEventId
        /// </summary>
        public UsersEvent GetUsersEventById(int usersEventId)
        {
            return context.UsersEvents.Find(usersEventId);
        } 

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        public void UpdateUsersEvent(UsersEvent userEvent) 
        {
            context.Entry(userEvent).State = EntityState.Modified;
        }

        public List<UsersEvent> GetAllAttending(Guid eid)
        {
            List<UsersEvent> allAttending = context.UsersEvents.Include(x => x.User).Where(x => x.EventId == eid).ToList();

            return allAttending;
        }

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        public void DeleteUsersEvent(int userEventId)
        {
            UsersEvent userEvent = context.UsersEvents.Find(userEventId);
            context.Entry(userEvent).State = EntityState.Deleted;
            context.UsersEvents.Remove(userEvent);
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
            if (!this.disposed && disposing)
            {
                context.Dispose();
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