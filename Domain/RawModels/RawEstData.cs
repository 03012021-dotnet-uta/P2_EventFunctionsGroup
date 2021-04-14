using System;

namespace Domain.RawModels
{
    public class RawEstData
    {
        public string EventType { get; set; }
        public decimal TicketsSold { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal AverageRevenue { get; set; }
    }
}