using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public List<EventType> InitTypes(List<string> types)
        {
            foreach(string t in types)
            {
                EventType et = mapper.StringToEventType(t);
                testRepo.InitEventTypes(et);
            }
            List<EventType> allTypes = testRepo.GetAllEventTypes();
            return allTypes;
        }

        public User GetUserByEmail(string email, string password)
        {
            if(IfUserExists(email))
            {
                return null;
            }
            User getUser = testRepo.GetUserByEmail(email);
            byte[] enteredPassword = mapper.PasswordHash(password, getUser.PasswordSalt);
            if(CompareHash(enteredPassword, getUser.Password))
            {
                return getUser;
            }
            else
            {
                return null;
            }
        }

        private bool CompareHash(byte[] enteredPassword, byte[] password)
        {
            if(password.Length != enteredPassword.Length)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < password.Length; i++)
                {
                    if(password[i] != enteredPassword[i])
                    {
                        return false;
                    }
                }
                return true;
            }
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
    }
}
