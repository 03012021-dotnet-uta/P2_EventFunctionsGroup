using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    public class UserLogicTest
    {
        DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        [Fact]
        public void Test_CreateUser()
        {
            RawUser testUser = new RawUser();
            testUser.email = "test@gmail.com";
            testUser.firstName = "fame";
            testUser.lastName = "lame";
            testUser.password = "test";

            User insertBack = new User();
            User getUser = new User();
            
            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                UserRepo userRepo = new UserRepo(context);
                UserLogic test = new UserLogic(userRepo);
                insertBack = test.CreateUser(testUser);
            }

            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();
                getUser = context1.Users.Where(x => x.Password == insertBack.Password).FirstOrDefault();
            }
            
            Assert.Equal(insertBack.FName, getUser.FName);
        }
    }
}
