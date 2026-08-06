public interface IAuthenticationService
{
    Task LoginAsync(string email, string password);
}