namespace Application.Authentication.DTOs;

/// <summary>
/// Request de login
/// </summary>
public record LoginRequest(
    string Email,
    string Password
);