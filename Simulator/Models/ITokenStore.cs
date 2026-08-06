public interface ITokenStore
{
    string? Token { get; set; }

    bool IsAuthenticated { get; }
}