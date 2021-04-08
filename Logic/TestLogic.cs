using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.RawModels;
using Repository;

namespace Logic
{
    public class TestLogic
    {
        private readonly TestRepository testRepo;
        private readonly Mapper mapper = new Mapper();
        public TestLogic(TestRepository r)
        {
            testRepo = r;
        }

        public string test()
        {
            return "test";
        }

        public List<User> GetUsers() {
            List<User> users = testRepo.GetUsers();

            return users;
        }

        public User CreateUser(RawUser user)
        {
            User newUser = mapper.RawToUser(user);
            newUser = testRepo.AddUser(newUser);
            return newUser;
        }
    }
}
