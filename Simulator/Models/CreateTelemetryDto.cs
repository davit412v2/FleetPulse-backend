namespace Simulator.Models;

public record CreateTelemetryDto
{
    public required Guid VehicleId { get; init; }
    public Guid? RouteId { get; init; }
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
    public required decimal Speed { get; init; }
    public required decimal FuelLevel { get; init; }
    public required decimal Temperature { get; init; }
    public DateTime? Timestamp { get; init; }
}