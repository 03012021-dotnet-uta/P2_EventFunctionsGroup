using System;
using System.Collections.Generic;
using Domain.Models;
using PizzaBox.Repository;

namespace Logic
{
    public class TestLogic
    {
        private readonly TestRepository customerRepo;
        public TestLogic(TestRepository r)
        {
            customerRepo = r;
        }

        public string test()
        {
            return "test";
        }

        public List<User> GetUsers() {
            List<User> users = customerRepo.GetUsers();

            return users;
        }
    }
}
