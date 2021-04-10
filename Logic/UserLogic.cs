using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Repository;
using Repository.Repos;

namespace Logic
{
    public class UserLogic
    {
        private readonly UserRepo userRepo;
        private readonly Mapper mapper = new Mapper();
        public UserLogic(UserRepo r)
        {
            userRepo = r;
        }

        public string test()
        {
            return "test";
        }

        /// <summary>
        /// Getse all users from the database
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers() {
            List<User> users = userRepo.GetAllUsers();

            return users;
        }

        /// <summary>
        /// Creates a new user. Maps from raw to user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User CreateUser(RawUser user)
        {
            if(IfUserExists(user.email))
            {
                return null;
            }
            User newUser = mapper.RawToUser(user);
            newUser = userRepo.InsertUser(newUser);
            return newUser;
        }

        /// <summary>
        /// Check is a user's email is in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IfUserExists(string email)
        {
            User getUser = userRepo.GetUserByEmail(email.ToLower());
            if(getUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Get all user information from ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByID(Guid id)
        {
            User getUser = await Task.Run(() => userRepo.GetUserByID(id));
            return getUser;
        }

        /// <summary>
        /// Gets all user information from email(Login)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email, string password)
        {
            if(!IfUserExists(email))
            {
                return null;
            }
            User getUser = userRepo.GetUserByEmail(email);
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

        /// <summary>
        /// Compares 2 hashed passwords
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>        
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
    }
}
