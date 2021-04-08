using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class Location
    {
        public Location()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int MaxCapacity { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
