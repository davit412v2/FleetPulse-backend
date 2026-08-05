namespace Application.Authentication.Interfaces;

/// <summary>
/// Servicio para hash de contraseñas
/// </summary>
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}