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
    public class UsersEventRepoTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test5")
            .Options;

        [Fact]
        public void Test_UsersEventCreate()
        {
            UsersEventRepo testRepo = new UsersEventRepo();
            Assert.NotNull(testRepo);
        }

        [Fact]
        public void Test_InsertUsersEvent()
        {
            Event testEvent = new Event();
            testEvent.Name = "Test Event";
            User testUser = new User();
            testUser.FName = "Test User";
            UsersEvent testObj = new UsersEvent();
            testObj.Event = testEvent;
            testObj.EventId = testEvent.Id;
            testObj.User = testUser;
            testObj.UserId = testUser.Id;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                UsersEventRepo testRepo = new UsersEventRepo(context);
                testRepo.InsertUsersEvent(testObj);
            }
            
            UsersEvent getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                getObj = context1.UsersEvents.FirstOrDefault(x => Guid.Equals(x.EventId, testEvent.Id));
            }
            
            Assert.Equal(testObj.UserId, getObj.UserId);
        }

        [Fact]
        public void Test_GetAllUsersEvents()
        {
            Event testEvent = new Event();
            testEvent.Name = "Test Event";
            User testUser = new User();
            testUser.FName = "Test User";
            User testUser1 = new User();
            testUser1.FName = "Test User";
            UsersEvent testObj = new UsersEvent();
            testObj.Event = testEvent;
            testObj.EventId = testEvent.Id;
            testObj.User = testUser;
            testObj.UserId = testUser.Id;
            UsersEvent testObj1 = new UsersEvent();
            testObj1.Event = testEvent;
            testObj1.EventId = testEvent.Id;
            testObj1.User = testUser1;
            testObj1.UserId = testUser1.Id;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<UsersEvent>(testObj);
                context.Add<UsersEvent>(testObj1);
                context.SaveChanges();
            }
            
            List<UsersEvent> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UsersEventRepo testRepo = new UsersEventRepo(context1);

                getList = testRepo.GetAllUsersEvents();
            }
            
            Assert.Equal(2, getList.Count);
        }

        [Fact]
        public void Test_DeleteEventType()
        {
            Event testEvent = new Event();
            testEvent.Name = "Test Event";
            User testUser = new User();
            testUser.FName = "Test User";
            User testUser1 = new User();
            testUser1.FName = "Test User";
            UsersEvent testObj = new UsersEvent();
            testObj.Event = testEvent;
            testObj.EventId = testEvent.Id;
            testObj.User = testUser;
            testObj.UserId = testUser.Id;
            UsersEvent testObj1 = new UsersEvent();
            testObj1.Event = testEvent;
            testObj1.EventId = testEvent.Id;
            testObj1.User = testUser1;
            testObj1.UserId = testUser1.Id;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<UsersEvent>(testObj);
                context.Add<UsersEvent>(testObj1);
                context.SaveChanges();
            }
            
            List<UsersEvent> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UsersEventRepo testRepo = new UsersEventRepo(context1);

                testRepo.DeleteUsersEvent(testUser.Id, testEvent.Id);
                getList = context1.UsersEvents.ToList();
            }
            
            Assert.Equal(1, getList.Count);
        }

        [Fact]
        public void Test_GetUsersEventByIds()
        {
            Event testEvent = new Event();
            testEvent.Name = "Test Event";
            User testUser = new User();
            testUser.FName = "Test User";
            UsersEvent testObj = new UsersEvent();
            testObj.Event = testEvent;
            testObj.EventId = testEvent.Id;
            testObj.User = testUser;
            testObj.UserId = testUser.Id;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<UsersEvent>(testObj);
                context.SaveChanges();
            }
            
            UsersEvent getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UsersEventRepo testRepo = new UsersEventRepo(context1);

                getObj = testRepo.GetUsersEventById(testUser.Id, testEvent.Id);
            }
            
            Assert.Equal(testObj.User.FName, getObj.User.FName);
        }

        [Fact]
        public void Test_GetAllAttending()
        {
            Event testEvent = new Event();
            testEvent.Name = "Test Event";
            User testUser = new User();
            testUser.FName = "Test User";
            User testUser1 = new User();
            testUser1.FName = "Test User";
            UsersEvent testObj = new UsersEvent();
            testObj.Event = testEvent;
            testObj.EventId = testEvent.Id;
            testObj.User = testUser;
            testObj.UserId = testUser.Id;
            UsersEvent testObj1 = new UsersEvent();
            testObj1.Event = testEvent;
            testObj1.EventId = testEvent.Id;
            testObj1.User = testUser1;
            testObj1.UserId = testUser1.Id;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<UsersEvent>(testObj);
                context.Add<UsersEvent>(testObj1);
                context.SaveChanges();
            }
            
            List<UsersEvent> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UsersEventRepo testRepo = new UsersEventRepo(context1);

                getList = testRepo.GetAllAttending(testEvent.Id);
            }
            
            Assert.Equal(2, getList.Count);
        }
    }
}
