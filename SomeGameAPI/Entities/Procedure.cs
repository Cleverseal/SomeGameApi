using System;

namespace SomeGameAPI.Entities
{
    public class Procedure : BaseEntity
    {
        public int UserId { get; set; }

        public int PatientId { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Comments { get; set; }

        public string Status { get; set; }

        public int Heartrate { get; set; }

        public float Temperature { get; set; }
    }
}
