using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de la entidad Telemetry
/// </summary>
public class TelemetryConfiguration : IEntityTypeConfiguration<Telemetry>
{
    public void Configure(EntityTypeBuilder<Telemetry> builder)
    {
        builder.ToTable("Telemetry");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.VehicleId)
            .IsRequired();

        builder.Property(t => t.RouteId)
            .IsRequired(false);

        builder.Property(t => t.Latitude)
            .HasColumnType("decimal(10,7)")
            .IsRequired();

        builder.Property(t => t.Longitude)
            .HasColumnType("decimal(10,7)")
            .IsRequired();

        builder.Property(t => t.Speed)
            .HasColumnType("decimal(6,2)")
            .IsRequired();

        builder.Property(t => t.FuelLevel)
            .HasColumnType("decimal(8,2)")
            .IsRequired();

        builder.Property(t => t.Temperature)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(t => t.Timestamp)
            .IsRequired();

        // Relationships
        builder.HasOne(t => t.Vehicle)
            .WithMany()
            .HasForeignKey(t => t.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Route)
            .WithMany()
            .HasForeignKey(t => t.RouteId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes para optimizar consultas por vehículo y timestamp
        builder.HasIndex(t => t.VehicleId);
        builder.HasIndex(t => t.Timestamp);
        builder.HasIndex(t => new { t.VehicleId, t.Timestamp });
    }
}