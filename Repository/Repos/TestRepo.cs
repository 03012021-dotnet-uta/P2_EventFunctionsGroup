  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Data.SqlClient;
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
    }
}