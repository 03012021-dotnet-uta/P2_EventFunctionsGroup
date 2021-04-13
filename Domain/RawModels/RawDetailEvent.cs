using System;

namespace Domain.RawModels
{
    public class RawDetailEvent
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public string Manager { get; set; }
        public int CurrentAttending { get; set; }
        public int Capacity { get; set; }
        public decimal TicketPrice { get; set; }
        public string LocationMap { get; set; }
    }
}