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
    /// This is the Repo class for EventTypes
    /// It implements the CRUD functions from its
    /// respective interface.
    /// It also implements Disposal pattern since it
    /// contains unmanaged resources.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public class EventTypeRepo : IEventTypeRepository, IDisposable
    {
    
        /// <summary>
        /// Context variable that will be injected and used by business logic
        /// </summary>
        private readonly EventFunctionsContext context;


        /// <summary>
        /// Empty constructor to instantiate the context
        /// and then assign to context variable
        /// </summary>
        public EventTypeRepo() 
        {
            context = new EventFunctionsContext();
        }

        /// <summary>
        /// Pass in context using Dependency Injection
        /// and assign to context variable
        /// </summary>
        public EventTypeRepo(EventFunctionsContext eventFunctionsContext) 
        {
            context = eventFunctionsContext;
        }

        public void InsertEventType(EventType eventType) 
        {
            context.EventTypes.Add(eventType);
        }

        public List<EventType> GetAllEventTypes() 
        {
            return context.EventTypes.ToList();
        }

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        public void UpdateEventType(EventType eventType) 
        {
            context.Entry(eventType).State = EntityState.Modified;
        }

        public void DeleteEventType(int eventTypeId)
        {
            EventType eventType = context.EventTypes.Find(eventTypeId);
            context.Entry(eventType).State = EntityState.Deleted;
            context.EventTypes.Remove(eventType);
        }
        
        public async Task<EventType> GetEventTypeByIDAsync(Guid eventType)
        {
            EventType getType = await Task.Run(() => context.EventTypes.FirstOrDefault(n => Guid.Equals(n.Id, eventType)));
            return getType;
        }

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