using Microsoft.EntityFrameworkCore;
using WinemakerAPI.Entities;

namespace WinemakerAPI.DbContexts
{
    public class WineCollectionContext : DbContext
    {
        public WineCollectionContext(DbContextOptions<WineCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<WineMaker> WineMakers { get; set; }
        public DbSet<WineBottle> WineBottles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relation between bottle and win maker 
            modelBuilder.Entity<WineMaker>()
                .HasMany(w => w.WineBottles)
                .WithOne(b => b.WineMaker)
                .HasForeignKey(b => b.WineMakerId);
        }
    }
}
