using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routes");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Origin)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Destination)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Distance)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(r => r.EstimatedTimeMinutes)
            .IsRequired();

        // Relación 1:N con RoutePoints
        builder.HasMany(r => r.RoutePoints)
            .WithOne(rp => rp.Route)
            .HasForeignKey(rp => rp.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}