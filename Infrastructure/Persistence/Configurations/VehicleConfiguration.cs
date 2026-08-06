using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Plate)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.Brand)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Year)
            .IsRequired();

        builder.Property(v => v.FuelCapacity)
            .IsRequired()
            .HasPrecision(10, 2);

        // Relación N:1 con Driver
        builder.HasOne(v => v.Driver)
            .WithMany(d => d.Vehicles)
            .HasForeignKey(v => v.DriverId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}