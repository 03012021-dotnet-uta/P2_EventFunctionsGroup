using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.RawModels;
using Repository;

namespace Logic
{
    public class TestLogic
    {
        private readonly TestRepository customerRepo;
        private readonly Mapper mapper = new Mapper();
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

        public User CreateUser(RawUser user)
        {
            User newUser = new User();//mapper.RawToUser(user);

            return newUser;
        }
    }
}
