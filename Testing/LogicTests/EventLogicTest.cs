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
            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.MinValue;
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

            Assert.Equal(2, allEvents.Count);
        }

        [Fact]
        public async Task Test_GetUpcomingEventsAsync()
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
            testEvent1.Date = DateTime.MinValue;
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
        public async Task Test_GetAllPreviousEventsAsync()
        {
            User testUser = new User();
            testUser.Events = new List<Event>();
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
            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.MinValue;
            testEvent1.Description = "This is just a test";
            testEvent1.Location = testLocation;
            testEvent1.Capacity = 10;
            testEvent1.Revenue = 10;
            testEvent1.Manager = testUser;
            testEvent1.EventType = testEventType;
            testEvent1.Users = new List<User>();

            testEvent.Users.Add(testUser);
            testEvent1.Users.Add(testUser);
            testUser.Events.Add(testEvent);
            testUser.Events.Add(testEvent1);

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<Event>(testEvent);
                context.Add<Event>(testEvent1);
                
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

                allEvents = await Task.Run(() => test.GetAllPreviousEventsAsync(testUser.Id));
            }

            Assert.Equal(1, allEvents.Count);
        }

        [Fact]
        public async Task Test_GetAllSignedUpEventsAsync()
        {
            User testUser = new User();
            testUser.Events = new List<Event>();
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
            Event testEvent1 = new Event();
            testEvent1.Name = "test";
            testEvent1.Date = DateTime.MinValue;
            testEvent1.Description = "This is just a test";
            testEvent1.Location = testLocation;
            testEvent1.Capacity = 10;
            testEvent1.Revenue = 10;
            testEvent1.Manager = testUser;
            testEvent1.EventType = testEventType;
            testEvent1.Users = new List<User>();

            testEvent.Users.Add(testUser);
            testEvent1.Users.Add(testUser);
            testUser.Events.Add(testEvent);
            testUser.Events.Add(testEvent1);

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<Event>(testEvent);
                context.Add<Event>(testEvent1);
                
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

                allEvents = await Task.Run(() => test.GetAllSignedUpEventsAsync(testUser.Id));
            }

            Assert.Equal(1, allEvents.Count);
        }

        [Fact]
        public async Task Test_GetEventById()
        {
            User testUser = new User();
            testUser.Events = new List<Event>();
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

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<Event>(testEvent);
                
                context.SaveChanges();
            }
            
            RawDetailEvent getEvent;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                getEvent = await Task.Run(() => test.GetEventById(testEvent.Id));
            }

            Assert.Equal(testEvent.Name, getEvent.Name);
        }

        [Fact]
        public async Task Test_GetAllReviews()
        {
            User testUser = new User();
            testUser.Events = new List<Event>();
            User testUser1 = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test event";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            testEvent.Users = new List<User>();

            Review testReivew = new Review();
            testReivew.Rating = 3;
            testReivew.Description = "This is a test review";
            testReivew.Event = testEvent;
            testReivew.User = testUser;
            Review testReivew1 = new Review();
            testReivew1.Rating = 4;
            testReivew1.Description = "This is a test review";
            testReivew1.Event = testEvent;
            testReivew1.User = testUser1;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.Add<Event>(testEvent);
                context.Add<Review>(testReivew);
                context.Add<Review>(testReivew1);
                
                context.SaveChanges();
            }
            
            List<RawReviewToFE> allReviews;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                allReviews = await Task.Run(() => test.GetAllReviews(testEvent.Id));
            }

            Assert.Equal(2, allReviews.Count);
        }

        [Fact]
        public async Task Test_CreateReview()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test event";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            testEvent.Users = new List<User>();

            RawReview testReview = new RawReview();
            testReview.EventId = testEvent.Id;
            testReview.UserId = testUser.Id;
            testReview.Rating = 3;
            testReview.Description = "This is a test raw review";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<Event>(testEvent);
                
                context.SaveChanges();
            }
            
            RawReviewToFE allReviews;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                allReviews = await Task.Run(() => test.CreateReview(testReview));
            }

            Assert.Equal(testEvent.Name, allReviews.Event);
        }

        [Fact]
        public async Task Test_EventSignUp()
        {
            User testUser = new User();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test event";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            testEvent.Users = new List<User>();

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<Event>(testEvent);
                
                context.SaveChanges();
            }
            
            bool signedUp;
            List<UsersEvent> userSigned;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                signedUp = await Task.Run(() => test.EventSignUp(testUser.Id, testEvent.Id));
                userSigned = context1.UsersEvents.ToList();
            }
            Assert.Equal(1, userSigned.Count);
        }

        [Fact]
        public async Task Test_UnregisterFromEvent()
        {
            User testUser = new User();
            testUser.Events = new List<Event>();
            User testUser1 = new User();
            testUser1.Events = new List<Event>();
            Location testLocation = new Location();
            EventType testEventType = new EventType();

            Event testEvent = new Event();
            testEvent.Name = "test";
            testEvent.Date = DateTime.MaxValue;
            testEvent.Description = "This is just a test event";
            testEvent.Location = testLocation;
            testEvent.Capacity = 10;
            testEvent.Revenue = 10;
            testEvent.Manager = testUser;
            testEvent.EventType = testEventType;
            testEvent.Users = new List<User>();

            testEvent.Users.Add(testUser);
            testEvent.Users.Add(testUser1);
            testUser.Events.Add(testEvent);
            testUser1.Events.Add(testEvent);

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.Add<Event>(testEvent);
                
                context.SaveChanges();
            }
            
            bool signedUp;
            List<UsersEvent> userSigned;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                                
                EventRepo eventRepo = new EventRepo(context1);
                ReviewRepo reviewRepo = new ReviewRepo(context1);
                UsersEventRepo usersEventRepo = new UsersEventRepo(context1);
                UserRepo userRepo = new UserRepo(context1);
                EventLogic test = new EventLogic(eventRepo, userRepo, usersEventRepo,reviewRepo);

                signedUp = await Task.Run(() => test.UnregisterFromEvent(testUser.Id, testEvent.Id));
                userSigned = context1.UsersEvents.ToList();
            }
            Assert.Equal(1, userSigned.Count);
        }

    }
}
