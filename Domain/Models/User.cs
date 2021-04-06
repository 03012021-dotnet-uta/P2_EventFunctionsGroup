using System;

namespace Domain.Models
{
    public class User
    {
        public Guid UserID { get; set; } = Guid.NewGuid();
        public string Fname { get; set; }
        public string  Lname { get; set; }
        public bool IsManager { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
