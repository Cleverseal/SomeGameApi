using System.ComponentModel.DataAnnotations;

namespace SomeGameAPI.Entities
{
    public class User : BaseEntity 
    {
        public int DepId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
