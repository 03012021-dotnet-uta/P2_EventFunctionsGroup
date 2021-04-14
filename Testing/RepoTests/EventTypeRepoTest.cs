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
    public class EventTypeRepoTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test4")
            .Options;

        [Fact]
        public void Test_EventTypeCreate()
        {
            EventType testEventType = new EventType();
            Assert.NotNull(testEventType);
        }

        [Fact]
        public void Test_InsertEventType()
        {
            EventType testEventType = new EventType();
            testEventType.Name = "Test Event Type";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                EventTypeRepo eventTypeRepo = new EventTypeRepo(context);
                eventTypeRepo.InsertEventType(testEventType);
            }
            
            EventType getEventType;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                getEventType = context1.EventTypes.FirstOrDefault(x => Guid.Equals(x.Id, testEventType.Id));
            }
            
            Assert.Equal(testEventType.Name, getEventType.Name);
        }

        [Fact]
        public void Test_GetAllEventTypes()
        {
            EventType testEventType = new EventType();
            testEventType.Name = "Test Event Type";
            EventType testEventType1 = new EventType();
            testEventType.Name = "Test Event Type";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<EventType>(testEventType);
                context.Add<EventType>(testEventType1);
                context.SaveChanges();
            }
            
            List<EventType> getEventType;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);

                getEventType = eventTypeRepo.GetAllEventTypes();
            }
            
            Assert.Equal(2, getEventType.Count);
        }

        [Fact]
        public void Test_UpdateEventType()
        {
            EventType testEventType = new EventType();
            testEventType.Name = "Test Event Type";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<EventType>(testEventType);
                context.SaveChanges();
            }
            testEventType.Name = "New Test Event";
            EventType getEventType;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);

                eventTypeRepo.UpdateEventType(testEventType);
                eventTypeRepo.Save();
                getEventType = context1.EventTypes.FirstOrDefault(x => Guid.Equals(x.Id, testEventType.Id));
            }
            
            Assert.Equal(testEventType.Name, getEventType.Name);
        }

        [Fact]
        public void Test_DeleteEventType()
        {
            EventType testEventType = new EventType();
            testEventType.Name = "Test Event Type";
            EventType testEventType1 = new EventType();
            testEventType.Name = "Test Event Type";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<EventType>(testEventType);
                context.Add<EventType>(testEventType1);
                context.SaveChanges();
            }
            
            List<EventType> getEventType;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);

                eventTypeRepo.DeleteEventType(testEventType.Id);
                eventTypeRepo.Save();
                getEventType = context1.EventTypes.ToList();
            }
            
            Assert.Equal(1, getEventType.Count);
        }

        [Fact]
        public async Task Test_GetEventTypeByIDAsync()
        {
            EventType testEventType = new EventType();
            testEventType.Name = "Test Event Type1";
            EventType testEventType1 = new EventType();
            testEventType.Name = "Test Event Type2";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<EventType>(testEventType);
                context.Add<EventType>(testEventType1);
                context.SaveChanges();
            }
            
            EventType getEventType;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                EventTypeRepo eventTypeRepo = new EventTypeRepo(context1);

                getEventType = await Task.Run(() => eventTypeRepo.GetEventTypeByIDAsync(testEventType.Id));
            }
            
            Assert.Equal(testEventType.Name, getEventType.Name);
        }
    }
}
