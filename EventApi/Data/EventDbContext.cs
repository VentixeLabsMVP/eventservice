using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventEntity>().HasData(
                new EventEntity { Id = 1, EventName = "Fredriks Event", Description = "crying in frontend", Price = 20 },
                new EventEntity
                {
                    Id = 2,
                    EventName = "Elins Event",
                    Description = "crying in money",
                    Price = 40
                }
            );
        }
    }

    public class EventEntity
    {
        public int Id { get; set; }
        public string EventName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = 0;
    }
}
