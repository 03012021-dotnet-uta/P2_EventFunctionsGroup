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
    public class LocationRepoTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test6")
            .Options;

        [Fact]
        public void Test_LocationRepoCreate()
        {
            LocationRepo testRepo = new LocationRepo();
            Assert.NotNull(testRepo);
        }

        [Fact]
        public void Test_InsertLocation()
        {
            Location testLoc = new Location();
            testLoc.Name = "Test Location";
            testLoc.Address = "111 some place";
            testLoc.Longtitude = 100;
            testLoc.Latitude = 100;
            testLoc.MaxCapacity = 10;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                LocationRepo testRepo = new LocationRepo(context);
                testRepo.InsertLocation(testLoc);
            }
            
            Location getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                getObj = context1.Locations.FirstOrDefault(x => Guid.Equals(x.Id, testLoc.Id));
            }
            
            Assert.Equal(testLoc.Name, getObj.Name);
        }

        [Fact]
        public void Test_GetAllLocations()
        {
            Location testLoc = new Location();
            testLoc.Name = "Test Location";
            testLoc.Address = "111 some place";
            testLoc.Longtitude = 100;
            testLoc.Latitude = 100;
            testLoc.MaxCapacity = 10;
            Location testLoc1 = new Location();
            testLoc1.Name = "Test Location";
            testLoc1.Address = "111 some place";
            testLoc1.Longtitude = 100;
            testLoc1.Latitude = 100;
            testLoc1.MaxCapacity = 10;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Location>(testLoc);
                context.Add<Location>(testLoc1);
                context.SaveChanges();
            }
            
            ICollection<Location> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                LocationRepo testRepo = new LocationRepo(context1);

                getList = testRepo.GetAllLocations();
            }
            
            Assert.Equal(2, getList.Count);
        }

        [Fact]
        public void Test_DeleteLocation()
        {
            Location testLoc = new Location();
            testLoc.Name = "Test Location";
            testLoc.Address = "111 some place";
            testLoc.Longtitude = 100;
            testLoc.Latitude = 100;
            testLoc.MaxCapacity = 10;
            Location testLoc1 = new Location();
            testLoc1.Name = "Test Location";
            testLoc1.Address = "111 some place";
            testLoc1.Longtitude = 100;
            testLoc1.Latitude = 100;
            testLoc1.MaxCapacity = 10;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Location>(testLoc);
                context.Add<Location>(testLoc1);
                context.SaveChanges();
            }
            
            ICollection<Location> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                LocationRepo testRepo = new LocationRepo(context1);

                testRepo.DeleteLocation(testLoc.Id);
                testRepo.Save();
                getList = context1.Locations.ToList();
            }
            
            Assert.Equal(1, getList.Count);
        }

        [Fact]
        public void Test_GetLocationById()
        {
            Location testLoc = new Location();
            testLoc.Name = "Test Location";
            testLoc.Address = "111 some place";
            testLoc.Longtitude = 100;
            testLoc.Latitude = 100;
            testLoc.MaxCapacity = 10;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Location>(testLoc);

                context.SaveChanges();
            }
            
            Location getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                LocationRepo testRepo = new LocationRepo(context1);

                getObj = testRepo.GetLocationById(testLoc.Id);
            }
            
            Assert.Equal(testLoc.Name, getObj.Name);
        }

        [Fact]
        public void Test_GetLocationByCoord()
        {
            Location testLoc = new Location();
            testLoc.Name = "Test Location";
            testLoc.Address = "111 some place";
            testLoc.Longtitude = 100;
            testLoc.Latitude = 100;
            testLoc.MaxCapacity = 10;
            Location testLoc1 = new Location();
            testLoc1.Name = "Test Location";
            testLoc1.Address = "111 some place";
            testLoc1.Longtitude = 100;
            testLoc1.Latitude = 100;
            testLoc1.MaxCapacity = 10;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Location>(testLoc);
                context.Add<Location>(testLoc1);
                context.SaveChanges();
            }
            
            Location getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                LocationRepo testRepo = new LocationRepo(context1);

                getObj = testRepo.GetLocationByCoord(testLoc.Longtitude, testLoc.Latitude);
            }
            
            Assert.Equal(testLoc.Name, getObj.Name);
        }

        [Fact]
        public void Test_GetLocationByAddress()
        {
            Location testLoc = new Location();
            testLoc.Name = "Test Location";
            testLoc.Address = "111 some place";
            testLoc.Longtitude = 100;
            testLoc.Latitude = 100;
            testLoc.MaxCapacity = 10;
            Location testLoc1 = new Location();
            testLoc1.Name = "Test Location";
            testLoc1.Address = "111 some place";
            testLoc1.Longtitude = 100;
            testLoc1.Latitude = 100;
            testLoc1.MaxCapacity = 10;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Location>(testLoc);
                context.Add<Location>(testLoc1);
                context.SaveChanges();
            }
            
            Location getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                LocationRepo testRepo = new LocationRepo(context1);

                getObj = testRepo.GetLocationByAddress(testLoc.Name, testLoc.Address);
            }
            
            Assert.Equal(testLoc.Name, getObj.Name);
        }
    }
}
