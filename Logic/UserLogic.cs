using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.RawModels;
using Repository;

namespace Logic
{
    public class UserLogic
    {
        private readonly TestRepository testRepo;
        private readonly Mapper mapper = new Mapper();
        public UserLogic(TestRepository r)
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
            if(IfUserExists(user.email))
            {
                return null;
            }
            User newUser = mapper.RawToUser(user);
            newUser = testRepo.AddUser(newUser);
            return newUser;
        }

        private bool IfUserExists(string email)
        {
            User getUser = testRepo.GetUserByEmail(email);
            if(getUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User GetUserByID(Guid id)
        {
            User getUser = testRepo.GetUserByID(id);
            return getUser;
        }
    }
}
