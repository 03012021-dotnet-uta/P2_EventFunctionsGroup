using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int MaxCapacity { get; set; }

        //public virtual ICollection<Event> Events { get; set; }
    }
}
