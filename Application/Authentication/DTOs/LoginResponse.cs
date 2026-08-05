namespace Application.Authentication.DTOs;

/// <summary>
/// Response de login
/// </summary>
public record LoginResponse(
    string Token,
    UserDto User
);

public record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    string Role
);