using System;
using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Domain.RawModels;

namespace Logic
{
    public class Mapper
    {
        public Mapper()
        {

        }

        internal User RawToUser(RawUser rUser)
        {
            User newUser = new User();
            using (var hmac = new HMACSHA512())
            {
                newUser.FName = rUser.firstName;
                newUser.LName = rUser.lastName;
                newUser.IsEventManager = false;
                newUser.Email = rUser.email.ToLower();
                newUser.PasswordSalt = hmac.Key;
                newUser.Password = PasswordHash(rUser.password, newUser.PasswordSalt);
            }
            return newUser;
        }

        public byte[] PasswordHash(string password, byte[] salt)
        {
            using HMACSHA512 hmac = new HMACSHA512(key: salt);

            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hashedPassword;
        }
    }
}