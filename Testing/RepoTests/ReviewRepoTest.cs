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
    public class ReviewRepoTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test7")
            .Options;

        [Fact]
        public void Test_ReviewRepoCreate()
        {
            ReviewRepo testRepo = new ReviewRepo();
            Assert.NotNull(testRepo);
        }

        [Fact]
        public void Test_InsertReview()
        {
            User testUser = new User();
            Event testEvent = new Event();
            Review testReview = new Review();
            testReview.Description = "This is a test review";
            testReview.Rating = 3;
            testReview.User = testUser;
            testReview.Event = testEvent;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                ReviewRepo testRepo = new ReviewRepo(context);
                testRepo.InsertReview(testReview);
            }
            
            Review getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                getObj = context1.Reviews.FirstOrDefault(x => Guid.Equals(x.Id, testReview.Id));
            }
            
            Assert.Equal(testReview.Description, getObj.Description);
        }

        [Fact]
        public void Test_GetAllReviews()
        {
            User testUser = new User();
            Event testEvent = new Event();
            Review testReview = new Review();
            testReview.Description = "This is a test review";
            testReview.Rating = 3;
            testReview.User = testUser;
            testReview.Event = testEvent;
            Review testReview1 = new Review();
            testReview1.Description = "This is a test review1";
            testReview1.Rating = 4;
            testReview1.User = testUser;
            testReview1.Event = testEvent;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Review>(testReview);
                context.Add<Review>(testReview1);
                context.SaveChanges();
            }
            
            ICollection<Review> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                ReviewRepo testRepo = new ReviewRepo(context1);

                getList = testRepo.GetAllReviews();
            }
            
            Assert.Equal(2, getList.Count);
        }

        [Fact]
        public void Test_DeleteReview()
        {
            User testUser = new User();
            Event testEvent = new Event();
            Review testReview = new Review();
            testReview.Description = "This is a test review";
            testReview.Rating = 3;
            testReview.User = testUser;
            testReview.Event = testEvent;
            Review testReview1 = new Review();
            testReview1.Description = "This is a test review1";
            testReview1.Rating = 4;
            testReview1.User = testUser;
            testReview1.Event = testEvent;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Review>(testReview);
                context.Add<Review>(testReview1);
                context.SaveChanges();
            }
            
            ICollection<Review> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                ReviewRepo testRepo = new ReviewRepo(context1);

                testRepo.DeleteReview(testReview.Id);
                testRepo.Save();

                getList = context1.Reviews.ToList();
            }
            
            Assert.Equal(1, getList.Count);
        }

        [Fact]
        public void Test_GetReviewAllByEventId()
        {
            User testUser = new User();
            Event testEvent = new Event();
            Review testReview = new Review();
            testReview.Description = "This is a test review";
            testReview.Rating = 3;
            testReview.User = testUser;
            testReview.Event = testEvent;
            Review testReview1 = new Review();
            testReview1.Description = "This is a test review1";
            testReview1.Rating = 4;
            testReview1.User = testUser;
            testReview1.Event = testEvent;

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<Review>(testReview);
                context.Add<Review>(testReview1);
                context.SaveChanges();
            }
            
            List<Review> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                ReviewRepo testRepo = new ReviewRepo(context1);

                getList = testRepo.GetReviewAllByEventId(testEvent.Id);
            }
            
            Assert.Equal(2, getList.Count);
        }
    }
}
