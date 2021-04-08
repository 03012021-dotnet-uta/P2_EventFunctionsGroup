using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class Event
    {
        public Event()
        {
            UsersEvents = new HashSet<UsersEvent>();
        }

        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Revenue { get; set; }
        public int TotalTicketsSold { get; set; }
        public decimal TotalCost { get; set; }
        public int Capacity { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual Location Location { get; set; }
//        public virtual User Manager { get; set; }
        public virtual ICollection<UsersEvent> UsersEvents { get; set; }
    }
}
