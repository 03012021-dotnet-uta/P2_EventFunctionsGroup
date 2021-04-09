using System;

namespace Domain.RawModels
{
    public class RawEvent
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public Guid ManagerID { get; set; }
        public Guid EventType { get; set; }
    }
}