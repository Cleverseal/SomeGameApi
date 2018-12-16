using SomeGameAPI.Models;

namespace SomeGameAPI.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
