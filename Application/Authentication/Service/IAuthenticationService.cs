using Application.Authentication.DTOs;
using Shared.Results;

namespace Application.Authentication.Services;

/// <summary>
/// Servicio de autenticación
/// </summary>
public interface IAuthenticationService
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}