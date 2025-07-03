using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts;

public class CityInfoDbContext : DbContext
{
    public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<PointOfInterest> PointOfInterest { get; set; } = null!;

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite();
    //     base.OnConfiguring(optionsBuilder);
    // }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasData(
                new City("Tehran")
                {
                    Id = 1,
                    Description = "This is Tehran"
                },
                new City("Shiraz")
                {
                    Id = 2,
                    Description = "This is Shiraz"
                },
                new City("Sirjan")
                {
                    Id = 3,
                    Description = "This is Sirjan"
                }
            );
        modelBuilder.Entity<PointOfInterest>()
            .HasData(
                new PointOfInterest("Academy Barnamenevisan")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Tell 021902-418"
                },
                new PointOfInterest("Shemiran")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "This is Shemiran"
                },
                new PointOfInterest("Meydan ToopKhoone")
                {
                    Id = 3,
                    CityId = 1,
                    Description = "This is ToopKhoone"
                }
                
                
                );

        base.OnModelCreating(modelBuilder);
    }
}