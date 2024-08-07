using Microsoft.EntityFrameworkCore;
using WinemakerAPI.Models;

namespace WinemakerAPI.DbContexts
{
    public class WineCollectionContext : DbContext
    {
        public WineCollectionContext(DbContextOptions<WineCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<Winemaker> Winemakers { get; set; }
        public DbSet<WineBottle> WineBottles { get; set; }
    }
}
