  using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Repository.Contexts;

namespace PizzaBox.Repository
{
    public class TestRepository
    {
        private readonly EventFunctionsContext context;
        //private readonly StoreContext sContext;

        public TestRepository(EventFunctionsContext c)//, StoreContext sc)
        {
            context = c;
            //sContext = sc;
        }

        public List<User> GetUsers() 
        {
            var allUsers = context.Users.ToList();

            return allUsers;
        }
    }
}