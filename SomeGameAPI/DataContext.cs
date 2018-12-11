using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SomeGameAPI.Entities;

namespace SomeGameAPI
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            this.InitDatabase();
        }

        private void InitDatabase()
        {
            if (!this.Users.Any())
            {
                this.Users.Add(new User { FirstName = "Test", LastName = "User", Username = "test", Password = "test" });
                this.SaveChanges();
            }
        }
    }
}
