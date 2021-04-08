using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class UsersEvent
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }

}
