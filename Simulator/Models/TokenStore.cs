namespace Simulator.Models;

public record TokenStore : ITokenStore
{
    public string? Token { get; set; }

    public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

}