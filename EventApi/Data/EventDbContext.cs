using EventApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace EventApi.Data
{

    //install packages. core, tools, design
    // : DbContext, generate const with options
    // add to primary const
    // define tables with DbSet<>
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<EventEntity> Events { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(
             new Address { Id = 1, StreetName = "Frontendgatan 1", City = "Stockholm" },
             new Address { Id = 2, StreetName = "Pengavägen 2", City = "Göteborg" }
            );

            modelBuilder.Entity<EventEntity>().HasData(
                new EventEntity
                {
                    Id = 1,
                    EventName = "Fredriks Event",
                    Description = "crying in frontend",
                    Price = 20,
                    AddressId = 1
                },
                new EventEntity
                {
                    Id = 2,
                    EventName = "Elins Event",
                    Description = "crying in money",
                    Price = 40,
                    AddressId = 2
                }
            );
        }
    }
}
