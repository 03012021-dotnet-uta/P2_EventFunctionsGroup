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
    public class EventLogicTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test2")
            .Options;
        
        [Fact]
        public async Task Test_CreateNewEvent()
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
            
            List<RawPreviewEvent> allEvents;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                allEvents = await Task.Run(() => test.GetUpcomingEventsAsync());
            }

            Assert.Equal(1, allEvents.Count);
        }

        [Fact]
        public async Task Test_GetAllAsync()
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
            
            List<RawPreviewEvent> allEvents;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                allEvents = await Task.Run(() => test.GetAllAsync());
            }

            Assert.Equal(1, allEvents.Count);
        }

    }
}
