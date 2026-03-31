using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.Entity<Tour>()
                .Property(t => t.PricePerPerson)
                .HasPrecision(18, 2);

            builder.Entity<Tour>()
                .Property(t => t.DiscountPercent)
                .HasPrecision(5, 2);

            builder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // Tour - Destination
            builder.Entity<Tour>()
                .HasOne(t => t.Destination)
                .WithMany(d => d.Tours)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tour - TourOperator
            builder.Entity<Tour>()
                .HasOne(t => t.TourOperator)
                .WithMany(o => o.Tours)
                .HasForeignKey(t => t.TourOperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tour - Category
            builder.Entity<Tour>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tours)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking - Tour
            builder.Entity<Booking>()
                .HasOne(b => b.Tour)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review - Tour
            builder.Entity<Review>()
                .HasOne(r => r.Tour)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            // FavoriteTour - Tour
            builder.Entity<FavoriteTour>()
                .HasOne(f => f.Tour)
                .WithMany()
                .HasForeignKey(f => f.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FavoriteTour>()
                .HasIndex(f => new { f.UserId, f.TourId })
                .IsUnique();

            // TourImage - Tour
            builder.Entity<TourImage>()
                .HasOne(i => i.Tour)
                .WithMany(t => t.Images)
                .HasForeignKey(i => i.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment - Booking
            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            Tourism.Data.Seeding.DataSeeder.Seed(builder);
        }
    }
}