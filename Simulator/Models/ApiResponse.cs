namespace Simulator.Models;

public record ApiResponse<T>
{
    public required T Data { get; init; }
    public required string Message { get; init; }
    public required bool isSuccess { get; init; }
}