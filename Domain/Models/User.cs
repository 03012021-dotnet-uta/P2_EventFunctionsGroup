using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsEventManager { get; set; }

        public virtual ICollection<Event> Events{ get; set; }
        public virtual ICollection<UsersEvent> UsersEvents { get; set; }

    }
}
