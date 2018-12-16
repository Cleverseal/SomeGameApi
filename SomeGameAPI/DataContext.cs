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

        public DbSet<Department> Departments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Procedure> TestingProcedures { get; set; }

        private void InitDatabase()
        {
            if (!this.Users.Any())
            {
                this.Users.Add(new User { Id = 1, Password = "test", FirstName = "test", Username = "Margo" });
                this.Users.Add(new User { Id = 2, Password = "test", SecondName = "heh",Username = "Mykyta" });
            }

            if (this.ChangeTracker.HasChanges()) this.SaveChanges();
        }
    }
}
