using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SomeGameAPI.Entities;

namespace SomeGameAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            this.InitDatabase();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Walkthrough> Walkthroughs { get; set; }

        private void InitDatabase()
        {
            if (!this.Users.Any())
            {
                this.Users.Add(new User { Id = 1, Password = "test", Username = "test", NickName = "Margo" });
                this.Users.Add(new User { Id = 2, Password = "test", Username = "heh", NickName = "Mykyta" });
            }
            
            if (!this.Walkthroughs.Select(x => x.UserId == 1).Any())
            {
                this.Walkthroughs.Add(new Walkthrough { Id = 1, Score = 28, Date = DateTime.Now, UserId = 1 });
                this.Walkthroughs.Add(new Walkthrough { Id = 2, Score = 57, Date = DateTime.Now, UserId = 1 });
                this.Walkthroughs.Add(new Walkthrough { Id = 3, Score = 2857, Date = DateTime.Now, UserId = 2 });
            }

            if (this.ChangeTracker.HasChanges()) this.SaveChanges();
        }
    }
}
