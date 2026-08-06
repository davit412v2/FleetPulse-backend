namespace Application.MasterData.DTOs;

public record VehicleDto(
    Guid Id,
    string Plate,
    string Brand,
    string Model,
    int Year,
    decimal FuelCapacity,
    Guid? DriverId,
    string? DriverName
);