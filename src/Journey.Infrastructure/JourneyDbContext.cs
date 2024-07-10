using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure;
public class JourneyDbContext : DbContext
{
    public DbSet<Trip> Trips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\04145906276\\Documents\\Estudo\\JourneyDatabase.db");
    }
}
