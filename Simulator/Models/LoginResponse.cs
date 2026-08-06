namespace Simulator.Models;

public record LoginResponse
{
    public required string Token { get; init; }
    public required UserDto User { get; init; }
}

public record UserDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required string Role { get; init; }
}