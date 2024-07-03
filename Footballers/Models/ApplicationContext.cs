using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Footballers.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var country1 = new Country() { Id = 1, Name = "USA" };
            var country2 = new Country() { Id = 2, Name = "Russia" };
            var country3 = new Country() { Id = 3, Name = "Italy" };

            var team1 = new Team() { Id = 1, Name = "PSG" };
            var team2 = new Team() { Id = 2, Name = "Real Madrid" };
            var team3 = new Team() { Id = 3, Name = "Zenit" };

            var footballer1 = new Footballer()
            { Id = 1, FirstName = "Christiano", SecondName = "Ronaldo", BirthDay = new DateOnly(1980, 5, 2), Gender = "Мужской", TeamId = 1, CountryId = 3 };
            var footballer2 = new Footballer()
            { Id = 2, FirstName = "Artem", SecondName = "Dzuba", BirthDay = new DateOnly(1975, 8, 12), Gender = "Мужской", TeamId = 3, CountryId = 2 };
            var footballer3 = new Footballer()
            { Id = 3, FirstName = "Zlatan", SecondName = "Ibrahimovich", BirthDay = new DateOnly(1978, 3, 22), Gender = "Мужской", TeamId = 2, CountryId = 1 };

            modelBuilder.Entity<Country>().HasData(
                    country1,
                    country2,
                    country3
            );

            modelBuilder.Entity<Team>().HasData(
                    team1,
                    team2,
                    team3
            );

            modelBuilder.Entity<Footballer>().HasData(
                    footballer1,
                    footballer2,
                    footballer3
            );

            

            
        }
    }
}
