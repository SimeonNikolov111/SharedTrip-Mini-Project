using SharedTrip.Models;

namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(entity =>
            {
                entity.HasKey(ut => new { ut.UserId, ut.TripId });

                entity.HasOne(ut => ut.User)
                    .WithMany(u => u.UserTrips)
                    .HasForeignKey(ut => ut.UserId);

                entity.HasOne(ut => ut.Trip)
                    .WithMany(u => u.UserTrips)
                    .HasForeignKey(ut => ut.TripId);
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }
    }
}
