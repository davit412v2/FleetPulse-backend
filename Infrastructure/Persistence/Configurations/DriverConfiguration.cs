using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.LicenseNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Ignore(d => d.FullName);

        // Relación 1:N con Vehicles
        builder.HasMany(d => d.Vehicles)
            .WithOne(v => v.Driver)
            .HasForeignKey(v => v.DriverId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}