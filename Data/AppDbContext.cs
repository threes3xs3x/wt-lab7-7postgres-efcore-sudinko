using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_sudinko.Models;

namespace wt_lab7_7postgres_efcore_sudinko.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Flight> Flights => Set<Flight>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                Id = 101,
                Number = "B2-101",
                Origin = "Minsk",
                Destination = "Warsaw",
                Price = 150,
                IsAvailable = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                Id = 102,
                Number = "B2-202",
                Origin = "Minsk",
                Destination = "Vilnius",
                Price = 80,
                IsAvailable = true,
                CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}