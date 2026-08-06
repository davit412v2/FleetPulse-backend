using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Alerta generada automáticamente por el sistema.
/// </summary>
public class Alert : BaseEntity
{

    public Guid VehicleId { get; set; }

    public Guid? TelemetryId { get; set; }

    public AlertType Type { get; set; }

    public AlertSeverity Severity { get; set; }


    public string Title { get; set; } = string.Empty;


    public string Message { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public DateTime Timestamp { get; set; }

    public bool IsRead { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;

    public virtual Telemetry? Telemetry { get; set; }
}