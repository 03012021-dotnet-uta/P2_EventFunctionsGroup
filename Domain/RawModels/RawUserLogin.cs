using System;

namespace Domain.RawModels
{
    public class RawUserLogin
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool IsEventManager { get; set; }
        public int Role { get; set; }
    }
}