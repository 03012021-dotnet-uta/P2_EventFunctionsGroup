using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Interfaces 
{
    public interface IUserRepository
    {

        // Create
        void InsertUser(User user);

        // Read
        ICollection<User> GetAllUsers();

        // Update
        void UpdateUser(User user);

        // Delete
        void DeleteUser(int userId);

        // Save
        void Save();

    }
}