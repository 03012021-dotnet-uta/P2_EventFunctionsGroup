using System;

namespace Domain.RawModels
{
    public class RawManagerEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int CurrentlyAttending { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TicketPrice { get; set; }
    }
}