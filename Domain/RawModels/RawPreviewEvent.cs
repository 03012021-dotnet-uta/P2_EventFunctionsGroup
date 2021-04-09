using System;

namespace Domain.RawModels
{
    public class RawPreviewEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}