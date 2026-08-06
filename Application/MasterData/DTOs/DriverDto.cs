namespace Application.MasterData.DTOs;

public record DriverDto(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string LicenseNumber,
    string Phone
);