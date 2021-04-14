using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Logic;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contexts;
using Repository.Repos;
using Xunit;

namespace Testing.LogicTests
{
    public class EventRepoTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test3")
            .Options;

        [Fact]
        public void Test_CreateEventRepo()
        {
            EventRepo eventRepo = new EventRepo();
            Assert.NotNull(eventRepo);
        }
        
        [Fact]
        public void Test_InsertEvent()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                EventRepo eventRepo = new EventRepo(context);
                eventRepo.InsertEvent(testEvent);
            }
            
            List<Event> getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                getEvent = context1.Events.ToList();
            }

            Assert.Equal(1, getEvent.Count);
        }

        [Fact]
        public void Test_GetAllEvents()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.MaxValue;
            testEvent1.Description = "This is just a test";
            testEvent1.Location = testLocation;
            testEvent1.Capacity = 10;
            testEvent1.Revenue = 10;
            testEvent1.Manager = testUser;
            testEvent1.EventType = testEventType;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Event>(testEvent);
                context.Add<Event>(testEvent1);
                
                context.SaveChanges();
            }
            
            List<Event> getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventRepo eventRepo = new EventRepo(context1);

                getEvent = eventRepo.GetAllEvents();
            }

            Assert.Equal(2, getEvent.Count);
        }

        [Fact]
        public void Test_DeleteEvent()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.MaxValue;
            testEvent1.Description = "This is just a test";
            testEvent1.Location = testLocation;
            testEvent1.Capacity = 10;
            testEvent1.Revenue = 10;
            testEvent1.Manager = testUser;
            testEvent1.EventType = testEventType;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Event>(testEvent);
                context.Add<Event>(testEvent1);
                
                context.SaveChanges();
            }
            
            List<Event> getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventRepo eventRepo = new EventRepo(context1);

                eventRepo.DeleteEvent(testEvent.Id);
                getEvent = context1.Events.ToList();
            }

            Assert.Equal(1, getEvent.Count);
        }

        [Fact]
        public void Test_GetEventById()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<Event>(testEvent);
                context.SaveChanges();
            }
            
            Event getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventRepo eventRepo = new EventRepo(context1);

                getEvent = eventRepo.GetEventByID(testEvent.Id);
            }

            Assert.Equal(testEvent.Name, getEvent.Name);
        }


        [Fact]
        public void Test_UpdateEvent()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<Event>(testEvent);
                context.SaveChanges();
                
            }
            
            Event getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventRepo eventRepo = new EventRepo(context1);
                testEvent.Name = "New Test";
                eventRepo.UpdateEvent(testEvent);
                getEvent = context1.Events.FirstOrDefault(x => Guid.Equals(x.Id, testEvent.Id));
            }

            Assert.Equal(testEvent.Name, getEvent.Name);
        }

        [Fact]
        public void Test_GetAllManagerEvents()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.MaxValue;
            testEvent1.Description = "This is just a test";
            testEvent1.Location = testLocation;
            testEvent1.Capacity = 10;
            testEvent1.Revenue = 10;
            testEvent1.Manager = testUser;
            testEvent1.EventType = testEventType;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.Add<Event>(testEvent);
                context.Add<Event>(testEvent1);
                
                context.SaveChanges();
            }
            
            List<Event> getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventRepo eventRepo = new EventRepo(context1);

                getEvent = eventRepo.GetAllManagerEvents(testUser.Id);
                
            }

            Assert.Equal(2, getEvent.Count);
        }

        [Fact]
        public void Test_GetAllAttending()
        {
            User testUser = new User();
            User testUser1 = new User();
            User testUser2 = new User();
            testUser.Events = new List<Event>();
            testUser1.Events = new List<Event>();
            testUser2.Events = new List<Event>();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            testEvent.Users = new List<User>();

            testEvent.Users.Add(testUser);
            testEvent.Users.Add(testUser1);
            testEvent.Users.Add(testUser2);
            testUser.Events.Add(testEvent);
            testUser1.Events.Add(testEvent);
            testUser2.Events.Add(testEvent);

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.Add<User>(testUser2);
                context.Add<Event>(testEvent);
                
                context.SaveChanges();
            }
            
            List<User> getUsers;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventRepo eventRepo = new EventRepo(context1);

                getUsers = eventRepo.GetAllAttending(testEvent.Id);
                
            }

            Assert.Equal(3, getUsers.Count);
        }
    }
}
