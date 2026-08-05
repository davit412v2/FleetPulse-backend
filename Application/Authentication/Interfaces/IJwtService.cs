using Domain.Entities;

namespace Application.Authentication.Interfaces;

/// <summary>
/// Servicio de JWT
/// </summary>
public interface IJwtService
{
    string GenerateToken(User user);
}