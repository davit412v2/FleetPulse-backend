namespace Application.Telemetry.DTOs;

/// <summary>
/// DTO para respuesta de telemetría
/// </summary>
public record TelemetryDto
{
    public Guid Id { get; init; }
    public Guid VehicleId { get; init; }
    public string VehiclePlate { get; init; } = string.Empty;
    public Guid? RouteId { get; init; }
    public string? RouteName { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
    public decimal Speed { get; init; }
    public decimal FuelLevel { get; init; }
    public decimal Temperature { get; init; }
    public DateTime Timestamp { get; init; }
}