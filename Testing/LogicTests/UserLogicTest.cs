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

        [Fact]
        public void Test_GetUsers()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "fame";
            testUser.LName = "lame";
            User testUser1 = new User();
            testUser1.Email = "test2@gmail.com";
            testUser1.FName = "fame1";
            testUser1.LName = "lame1";

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                UserRepo userRepo = new UserRepo(context);
                UserLogic test = new UserLogic(userRepo);
                
                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.SaveChanges();
            }

            List<User> allUsers;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo userRepo = new UserRepo(context1);
                UserLogic test = new UserLogic(userRepo);
                
                allUsers = test.GetUsers();
            }

            Assert.Equal(2, allUsers.Count);
        }

        [Fact]
        public async void Test_GetUserByID()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "fame";
            testUser.LName = "lame";

            User getUser = new User();
            
            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.SaveChanges();
            }

            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo userRepo = new UserRepo(context1);
                UserLogic test = new UserLogic(userRepo);

                getUser = await Task.Run(() => test.GetUserByID(testUser.Id));
            }
            
            Assert.Equal(testUser.FName, getUser.FName);
        }

        [Fact]
        public async void Test_GetUserByEmail()
        {
            string testEmail = "test@gmail.com";
            string password = "password";
            
            User testUser = new User();
            testUser.Email = testEmail;
            testUser.FName = "fame";
            testUser.LName = "lame";

            using (var hmac = new HMACSHA512())
            {
                testUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                testUser.PasswordSalt = hmac.Key;
            }

            RawUserLogin getUser = new RawUserLogin();
            
            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.SaveChanges();
            }

            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo userRepo = new UserRepo(context1);
                UserLogic test = new UserLogic(userRepo);

                getUser = await Task.Run(() => test.GetUserByEmail(testEmail, password));
            }
            
            Assert.Equal(testUser.FName, getUser.firstName);
        }
    }
}
