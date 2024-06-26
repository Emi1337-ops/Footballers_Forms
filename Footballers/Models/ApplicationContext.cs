﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Footballers.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Person> Persons { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Username = postgres; Password = 0000; Database = DefaultConnection;");
        }
    }
}
