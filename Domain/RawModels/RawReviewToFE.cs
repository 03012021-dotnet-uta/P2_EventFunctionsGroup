using System;

namespace Domain.RawModels
{
    public class RawReviewToFE
    {
        public string User { get; set; }
        public string Event { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}