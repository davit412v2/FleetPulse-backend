namespace Simulator.Models;

public record VehicleDto
{
    public required Guid Id { get; init; }
    public required string Plate { get; init; }
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required int Year { get; init; }
    public required decimal FuelCapacity { get; init; }
    public Guid? DriverId { get; init; }
    public string? DriverName { get; init; }
}