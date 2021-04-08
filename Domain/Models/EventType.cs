using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class EventType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        //public virtual ICollection<Event> Events { get; set; }
    }
}
