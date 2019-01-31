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

        public DbSet<Procedure> Procedures { get; set; }

        private void InitDatabase()
        {
            if (!this.Users.Any())
            {
                this.Users.Add(new User { Id = 1, Password = "testtest", FirstName = "test", Username = "Margo@gmail.com" });
                this.Users.Add(new User { Id = 2, Password = "test", SecondName = "heh",Username = "Mykyta" });
            }

            if (!this.Patients.Any())
            {
                this.Patients.Add(new Patient { Id = 1, FirstName = "Mykyta", LastName = "Tilinin", Diagnosis = "No", Birthday = new DateTime(1999, 6, 6) });
                this.Patients.Add(new Patient { Id = 2, FirstName = "Andrey", LastName = "Awesome", Diagnosis = "Autism", Birthday = new DateTime(1970, 1, 1) });
                this.Patients.Add(new Patient { Id = 3, FirstName = "Myshanya", LastName = "Petrosyan", Diagnosis = "No", Birthday = new DateTime(1999, 6, 6) });
                this.Patients.Add(new Patient { Id = 4, FirstName = "Tygran", LastName = "Petrosyan", Diagnosis = "Autism", Birthday = new DateTime(1998, 6, 6) });
                this.Patients.Add(new Patient { Id = 5, FirstName = "Leha", LastName = "Matematick", Diagnosis = "Yes", Birthday = new DateTime(1999, 6, 6) });
                this.Patients.Add(new Patient { Id = 6, FirstName = "Maxim", LastName = "Tatarin", Diagnosis = "No", Birthday = new DateTime(1999, 6, 26) });
            }

            if (this.ChangeTracker.HasChanges()) this.SaveChanges();
        }
    }
}
