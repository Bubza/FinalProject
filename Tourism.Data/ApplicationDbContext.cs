using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Tourism.Data.Models.Entities;

namespace Tourism.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TourOperator> TourOperators { get; set; }
        public DbSet<FavoriteTour> FavoriteTours { get; set; }
        public DbSet<TourImage> TourImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Fix decimal precision warnings
            builder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.Entity<Tour>()
                .Property(t => t.PricePerPerson)
                .HasPrecision(18, 2);

            builder.Entity<Tour>()
                .Property(t => t.DiscountPercent)
                .HasPrecision(5, 2);

            // Tour → Destination (many-to-one)
            builder.Entity<Tour>()
                .HasOne(t => t.Destination)
                .WithMany(d => d.Tours)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tour → TourOperator (many-to-one)
            builder.Entity<Tour>()
                .HasOne(t => t.TourOperator)
                .WithMany(o => o.Tours)
                .HasForeignKey(t => t.TourOperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Tour (many-to-one)
            builder.Entity<Booking>()
                .HasOne(b => b.Tour)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review → Tour (many-to-one)
            builder.Entity<Review>()
                .HasOne(r => r.Tour)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            // FavoriteTour → Tour (many-to-one)
            builder.Entity<FavoriteTour>()
                .HasOne(f => f.Tour)
                .WithMany()
                .HasForeignKey(f => f.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique constraint: един потребител не може да добави един маршрут два пъти
            builder.Entity<FavoriteTour>()
                .HasIndex(f => new { f.UserId, f.TourId })
                .IsUnique();

            // TourImage → Tour (many-to-one)
            builder.Entity<TourImage>()
                .HasOne(i => i.Tour)
                .WithMany(t => t.Images)
                .HasForeignKey(i => i.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            Tourism.Data.Seeding.DataSeeder.Seed(builder);
        }
    }
}
