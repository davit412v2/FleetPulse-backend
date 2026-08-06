namespace Application.MasterData.DTOs;

public record RouteDto(
    Guid Id,
    string Name,
    string Origin,
    string Destination,
    decimal Distance,
    int EstimatedTimeMinutes,
    List<RoutePointDto> RoutePoints
);
