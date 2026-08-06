namespace Application.MasterData.DTOs;

public record RoutePointDto(
    decimal Latitude,
    decimal Longitude,
    int Sequence
);