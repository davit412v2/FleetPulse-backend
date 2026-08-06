using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("Alerts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Message)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasPrecision(10,2);

        builder.Property(x => x.Timestamp)
            .IsRequired();

        builder.Property(x => x.IsRead)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Telemetry)
            .WithMany()
            .HasForeignKey(x => x.TelemetryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.VehicleId);

        builder.HasIndex(x => x.Type);

        builder.HasIndex(x => x.Timestamp);

        builder.HasIndex(x => x.IsRead);
    }
}