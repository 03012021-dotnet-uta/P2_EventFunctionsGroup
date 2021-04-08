using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User
    {
        public User()
        {
            UsersEvents = new HashSet<UsersEvent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsEventManager { get; set; }

        public virtual ICollection<UsersEvent> UsersEvents { get; set; }

    }
}
