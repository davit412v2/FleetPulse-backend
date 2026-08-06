namespace Application.Telemetry.DTOs;

/// <summary>
/// DTO para crear un registro de telemetría
/// </summary>
public record CreateTelemetryDto
{
    public Guid VehicleId { get; init; }
    public Guid? RouteId { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
    public decimal Speed { get; init; }
    public decimal FuelLevel { get; init; }
    public decimal Temperature { get; init; }
    public DateTime? Timestamp { get; init; } // Opcional, usa DateTime.UtcNow si es null
}