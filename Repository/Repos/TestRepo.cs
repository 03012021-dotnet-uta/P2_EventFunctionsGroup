  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;

namespace Repository
{
    public class TestRepository
    {
        private readonly EventFunctionsContext context;

        public TestRepository(EventFunctionsContext c)
        {
            context = c;
        }

        public List<User> GetUsers() 
        {
            var allUsers = context.Users.ToList();

            return allUsers;
        }

        public async Task<EventType> GetEventTypeByID(Guid eventType)
        {
            EventType getType = context.EventTypes.FirstOrDefault(n => Guid.Equals(n.Id, eventType));
            return getType;
        }

        public List<Event> GetAllEvents()
        {
            List<Event> allEvents = context.Events.ToList();
            return allEvents;
        }

        public void AddLocation(Location loc)
        {
            context.Add<Location>(loc);
            context.SaveChanges();
        }

        public Event AddEvent(Event newEvent)
        {
            context.Add<Event>(newEvent);
            context.SaveChanges();
            return context.Events.FirstOrDefault(n => Guid.Equals(n.Id, newEvent.Id));
        }

        public User AddUser(User newUser)
        {
            context.Add<User>(newUser);
            context.SaveChanges();
            var getBackuser = context.Users.FirstOrDefault(n => Guid.Equals(newUser.Id, n.Id));
            return getBackuser;
        }

        public List<Event> GetAllManagerEvents(Guid id)
        {
            var allEvents = context.Events.Where(n => Guid.Equals(n.Manager, id)).ToList();

            return allEvents;
        }

        public ICollection<Event> GetSignedUpEvents(Guid id)
        {
            List<ICollection<Event>> myEvents = context.Users.Where(n => Guid.Equals(n.Id, id)).Select(n => n.Events).ToList();
            return myEvents[0];
        }

        public async Task<User> GetUserByID(Guid id)
        {
            var user = context.Users.FirstOrDefault(n => Guid.Equals(n.Id, id));
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = context.Users.FirstOrDefault(n => n.Email == email);

            return user;
        }

        public int GetTotalAttend(Guid id)
        {
            var allUsers = context.Events.Where(n => Guid.Equals(n.Id, id)).Select(n => n.Users).ToList();
            int total = allUsers[0].Count();
            return total;
        }

        public Event GetEventByID(Guid id)
        {
            Event theEvent = context.Events.Include(x => x.Location).Include(x => x.EventType).Include(x => x.Manager).FirstOrDefault(n => Guid.Equals(n.Id, id));
            return theEvent;
        }

        public List<EventType> GetAllEventTypes()
        {
            List<EventType> allTypes = context.EventTypes.ToList();
            return allTypes;
        }

        public void InitEventTypes(EventType et)
        {
            context.Add<EventType>(et);
            context.SaveChanges();
        }

        public List<Event> GetUpcomingEvents(DateTime now)
        {
            List<Event> allEvents = context.Events.Where(n => n.Date > now).ToList();
            return allEvents;
        }

    }
}