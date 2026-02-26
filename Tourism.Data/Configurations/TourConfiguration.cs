using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Data.Models.Entities;
namespace Tourism.Data.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.HasOne(t => t.Destination)
                .WithMany(d => d.Tours)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.TourOperator)
                .WithMany(o => o.Tours)
                .HasForeignKey(t => t.TourOperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.PricePerPerson)
                .HasColumnType("decimal(18,2)");
        }
    }
}
