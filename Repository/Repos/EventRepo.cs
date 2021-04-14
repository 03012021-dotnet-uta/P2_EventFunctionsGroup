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
    /// This is the Repo class for Events
    /// It implements the CRUD functions from its
    /// respective interface.
    /// It also implements Disposal pattern since it
    /// contains unmanaged resources.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public class EventRepo : IEventRepository
    {
    
        /// <summary>
        /// Context variable that will be injected and used by business logic
        /// </summary>
        private readonly EventFunctionsContext context;


        /// <summary>
        /// Empty constructor to instantiate the context
        /// and then assign to context variable
        /// </summary>
        public EventRepo() 
        {
            context = new EventFunctionsContext();
        }

        /// <summary>
        /// Pass in context using Dependency Injection
        /// and assign to context variable
        /// </summary>
        public EventRepo(EventFunctionsContext eventFunctionsContext) 
        {
            context = eventFunctionsContext;
        }

        /// <summary>
        /// Insert a new item to context
        /// </summary>
        public Event InsertEvent(Event eventName) 
        {
            context.Add<Event>(eventName);
            Save();
            return context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).FirstOrDefault(n => Guid.Equals(n.Id, eventName.Id));
        }

        public List<Event> GetAllEvents() 
        {
            List<Event> allEvents = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).ToList();
            return allEvents;
        }

        public List<User> GetAllAttending(Guid eid)
        {
            var allAttending = context.Events.Include(x => x.Users).Where(x => Guid.Equals(x.Id, eid)).Select(x => x.Users).ToList();

            return allAttending[0].ToList();
        }

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        public void UpdateEvent(Event eventName) 
        {
            context.Entry(eventName).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        public bool DeleteEvent(Guid eventId)
        {
            Event tempEvent = context.Events.Find(eventId);
            context.Entry(tempEvent).State = EntityState.Deleted;
            context.Events.Remove(tempEvent);
            context.SaveChanges();
            return true;
        }

        public Event GetEventByID(Guid id)
        {
            Event theEvent = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).Include(x => x.Users).FirstOrDefault(n => Guid.Equals(n.Id, id));
            return theEvent;
        }

        public List<Event> GetAllManagerEvents(Guid id)
        {
            var allEvents = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).Where(n => Guid.Equals(n.Manager.Id, id)).ToList();

            return allEvents;
        }
        public List<Event> GetUpcomingEvents(DateTime now)
        {
            List<Event> allEvents = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).Where(n => n.Date > now).ToList();
            return allEvents;
        }
        public List<Event> GetPreviousEvents(DateTime now)
        {
            List<Event> allEvents = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).Where(n => n.Date < now).ToList();
            return allEvents;
        }

        public List<Event> GetEventsByEventType(Guid id)
        {
            List<Event> allEvents = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).Where(x => Guid.Equals(x.EventType.Id, id)).ToList();

            return allEvents;
        }

        public ICollection<Event> GetSignedUpEvents(Guid id)
        {
            List<ICollection<Event>> myEvents = context.Users.Where(n => Guid.Equals(n.Id, id)).Select(n => n.Events).ToList();
            
            return myEvents[0];
        }
        public int GetTotalAttend(Guid id)
        {
            var allUsers = context.Events.Where(n => Guid.Equals(n.Id, id)).Select(n => n.Users).ToList();
            int total = allUsers[0].Count;
            return total;
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