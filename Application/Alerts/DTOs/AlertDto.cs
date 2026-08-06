using Domain.Enums;

namespace Application.Alerts.DTOs;

public class AlertDto
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public string VehiclePlate { get; set; } = string.Empty;

    public Guid? TelemetryId { get; set; }

    public AlertType Type { get; set; }

    public AlertSeverity Severity { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public bool IsRead { get; set; }

    public DateTime Timestamp { get; set; }
}