using System.ComponentModel.DataAnnotations;

namespace SomeGameAPI.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
