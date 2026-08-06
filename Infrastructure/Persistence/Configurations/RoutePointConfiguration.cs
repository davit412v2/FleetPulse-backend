using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RoutePointConfiguration : IEntityTypeConfiguration<RoutePoint>
{
    public void Configure(EntityTypeBuilder<RoutePoint> builder)
    {
        builder.ToTable("RoutePoints");

        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.Latitude)
            .IsRequired()
            .HasPrecision(10, 7);

        builder.Property(rp => rp.Longitude)
            .IsRequired()
            .HasPrecision(10, 7);

        builder.Property(rp => rp.Sequence)
            .IsRequired();

        // Relación N:1 con Route
        builder.HasOne(rp => rp.Route)
            .WithMany(r => r.RoutePoints)
            .HasForeignKey(rp => rp.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}