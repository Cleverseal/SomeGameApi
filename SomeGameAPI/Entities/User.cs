using System.ComponentModel.DataAnnotations;

namespace SomeGameAPI.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
