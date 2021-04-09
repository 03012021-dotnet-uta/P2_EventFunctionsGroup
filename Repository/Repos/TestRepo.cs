  using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Repository.Contexts;

namespace Repository
{
    public class TestRepository
    {
        private readonly EventFunctionsContext context;

        public TestRepository(EventFunctionsContext c)
        {
            context = c;
        }

        public List<User> GetUsers() 
        {
            var allUsers = context.Users.ToList();

            return allUsers;
        }

        public User AddUser(User newUser)
        {
            context.Add<User>(newUser);
            context.SaveChanges();
            var getBackuser = context.Users.FirstOrDefault(n => Guid.Equals(newUser.Id, n.Id));
            return getBackuser;
        }

        public User GetUserByID(Guid id)
        {
            var user = context.Users.FirstOrDefault(n => Guid.Equals(n.Id, id));
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = context.Users.FirstOrDefault(n => n.Email == email);

            return user;
        }
    }
}