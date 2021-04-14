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
    public class ManagerLogicTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        [Fact]
        public async Task Test_CreateNewEvent()
        {
            User testUser = new User();
            EventType testEventType = new EventType();

            RawEvent testEvent = new RawEvent();
            testEvent.Name = "test";
            testEvent.Date = DateTime.Now.ToString();
            testEvent.Description = "This is just a test";
            testEvent.Capacity = 10;
            testEvent.Street = "1111 S Figueroa St";
            testEvent.City = "Los Angeles";
            testEvent.State = "CA";
            testEvent.ZipCode = 90015;
            testEvent.TicketPrice = 10;
            testEvent.ManagerID = testUser.Id;
            testEvent.EventType = testEventType.Id;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<EventType>(testEventType);
                
                context.SaveChanges();
            }
            
            Event createdEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);
                LocationRepo locationRepo = new LocationRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                ManagerLogic test = new ManagerLogic(userRepo, eventRepo, eventTypeRepo, locationRepo, usersEventRepo);

                createdEvent = await Task.Run(() => test.CreateNewEvent(testEvent));
            }

            Assert.Equal(testEvent.Name, createdEvent.Name);
        }

        [Fact]
        public void Test_GetAllEvents()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.Now;
            testEvent.Description = "This is just a test";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;

            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.Now;
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
            
            List<RawPreviewEvent> createdEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);
                LocationRepo locationRepo = new LocationRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                ManagerLogic test = new ManagerLogic(userRepo, eventRepo, eventTypeRepo, locationRepo, usersEventRepo);

                createdEvent = test.GetAllEvents(testUser.Id);

            }

            Assert.Equal(2, createdEvent.Count);
        }

        [Fact]
        public void Test_GetEventTypes()
        {
            EventType testEventType = new EventType();
            testEventType.Name = "Test Event";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<EventType>(testEventType);
                
                context.SaveChanges();
            }
            
            List<EventType> eventTypes;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);
                LocationRepo locationRepo = new LocationRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                ManagerLogic test = new ManagerLogic(userRepo, eventRepo, eventTypeRepo, locationRepo, usersEventRepo);

                eventTypes = test.GetEventTypes();
            }

            Assert.Equal(testEventType.Name, eventTypes[0].Name);
        }

        [Fact]
        public async Task Test_GetAllAttending()
        {
            EventType testEventType = new EventType();
            Location testLocation = new Location();
            User testManager = new User();
            
            Event testEvent = new Event();
            testEvent.Manager = testManager;
            testEvent.Location = testLocation;
            testEvent.EventType = testEventType;
            testEvent.Users = new List<User>();

            User testUser1 = new User();
            User testUser2 = new User();

            testEvent.Users.Add(testUser1);
            testEvent.Users.Add(testUser2);

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<Event>(testEvent);
                
                context.SaveChanges();
            }
            
            List<RawUser> eventTypes;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);
                LocationRepo locationRepo = new LocationRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                ManagerLogic test = new ManagerLogic(userRepo, eventRepo, eventTypeRepo, locationRepo, usersEventRepo);

                eventTypes = await Task.Run(() => test.GetAllAttending(testEvent.Id));
            }

            Assert.Equal(2, eventTypes.Count);
        }

    }
}
