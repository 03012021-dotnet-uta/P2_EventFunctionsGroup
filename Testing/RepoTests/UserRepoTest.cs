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
    public class UserRepoTest
    {
        readonly DbContextOptions<EventFunctionsContext> options = new DbContextOptionsBuilder<EventFunctionsContext>()
            .UseInMemoryDatabase(databaseName: "Test8")
            .Options;

        [Fact]
        public void Test_UserRepoCreate()
        {
            UserRepo testRepo = new UserRepo();
            Assert.NotNull(testRepo);
        }

        [Fact]
        public void Test_InsertUser()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "Test";
            testUser.LName = "Testing";
            testUser.IsEventManager = false;

            using (var hmac = new HMACSHA512())
            {
                testUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser.PasswordSalt = hmac.Key;
            }

            User returnUser;
            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                UserRepo testRepo = new UserRepo(context);
                returnUser = testRepo.InsertUser(testUser);
            }
            
            User getUser;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                getUser = context1.Users.FirstOrDefault(x => Guid.Equals(x.Id, testUser.Id));
            }
            
            Assert.Equal(returnUser.FName, getUser.FName);
        }

        [Fact]
        public void Test_GetAllUserss()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "Test";
            testUser.LName = "Testing";
            testUser.IsEventManager = false;
            User testUser1 = new User();
            testUser1.Email = "test@gmail.com";
            testUser1.FName = "Test";
            testUser1.LName = "Testing";
            testUser1.IsEventManager = false;

            using (var hmac = new HMACSHA512())
            {
                testUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser.PasswordSalt = hmac.Key;
            }
            using (var hmac = new HMACSHA512())
            {
                testUser1.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser1.PasswordSalt = hmac.Key;
            }

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.SaveChanges();
            }
            
            ICollection<User> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo testRepo = new UserRepo(context1);

                getList = testRepo.GetAllUsers();
            }
            
            Assert.Equal(2, getList.Count);
        }

        [Fact]
        public void Test_DeleteUser()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "Test";
            testUser.LName = "Testing";
            testUser.IsEventManager = false;
            User testUser1 = new User();
            testUser1.Email = "test@gmail.com";
            testUser1.FName = "Test";
            testUser1.LName = "Testing";
            testUser1.IsEventManager = false;

            using (var hmac = new HMACSHA512())
            {
                testUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser.PasswordSalt = hmac.Key;
            }
            using (var hmac = new HMACSHA512())
            {
                testUser1.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser1.PasswordSalt = hmac.Key;
            }

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.SaveChanges();
            }
            
            ICollection<User> getList;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo testRepo = new UserRepo(context1);

                testRepo.DeleteUser(testUser.Id);
                testRepo.Save();

                getList = context1.Users.ToList();
            }
            
            Assert.Equal(1, getList.Count);
        }

        [Fact]
        public void Test_GetUserByID()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "Test";
            testUser.LName = "Testing";
            testUser.IsEventManager = false;
            User testUser1 = new User();
            testUser1.Email = "test@gmail.com";
            testUser1.FName = "Test";
            testUser1.LName = "Testing";
            testUser1.IsEventManager = false;

            using (var hmac = new HMACSHA512())
            {
                testUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser.PasswordSalt = hmac.Key;
            }
            using (var hmac = new HMACSHA512())
            {
                testUser1.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser1.PasswordSalt = hmac.Key;
            }

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.SaveChanges();
            }
            
            User getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo testRepo = new UserRepo(context1);

                getObj = testRepo.GetUserByID(testUser.Id);
            }
            
            Assert.Equal(testUser.FName, getObj.FName);
        }

        [Fact]
        public void Test_GetUserByEmail()
        {
            User testUser = new User();
            testUser.Email = "test@gmail.com";
            testUser.FName = "Test";
            testUser.LName = "Testing";
            testUser.IsEventManager = false;
            User testUser1 = new User();
            testUser1.Email = "test@gmail.com";
            testUser1.FName = "Test";
            testUser1.LName = "Testing";
            testUser1.IsEventManager = false;

            using (var hmac = new HMACSHA512())
            {
                testUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser.PasswordSalt = hmac.Key;
            }
            using (var hmac = new HMACSHA512())
            {
                testUser1.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test"));
                testUser1.PasswordSalt = hmac.Key;
            }

            using(var context = new EventFunctionsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add<User>(testUser);
                context.Add<User>(testUser1);
                context.SaveChanges();
            }
            
            User getObj;
            using(var context1 = new EventFunctionsContext(options))
            {
                context1.Database.EnsureCreated();

                UserRepo testRepo = new UserRepo(context1);

                getObj = testRepo.GetUserByEmail(testUser.Email);
            }
            
            Assert.Equal(testUser.FName, getObj.FName);
        }
    }
}
