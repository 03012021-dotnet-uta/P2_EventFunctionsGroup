using System;
using System.Collections.Generic;

namespace Domain.Models
{

    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        //public int UserId { get; set; }
        //public int EventId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }

}