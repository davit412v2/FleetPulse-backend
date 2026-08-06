namespace Simulator.Models;

public record RouteDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Origin { get; init; }
    public required string Destination { get; init; }
    public required decimal Distance { get; init; }
    public required int EstimatedTimeMinutes { get; init; }
    public required List<RoutePointDto> RoutePoints { get; init; }
}

public record RoutePointDto
{
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
    public required int Sequence { get; init; }
}