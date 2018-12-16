using System;

namespace SomeGameAPI.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Diagnosis { get; set; }

        public string DiagnosisDetails { get; set; }
    }
}
