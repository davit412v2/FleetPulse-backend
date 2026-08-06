using Application.Authentication.DTOs;
using Application.Authentication.Interfaces;
using Domain.Interfaces;
using FluentValidation;
using Shared.Results;

namespace Application.Authentication.Services;

/// <summary>
/// Implementación del servicio de autenticación
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IValidator<LoginRequest> _validator;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IValidator<LoginRequest> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _validator = validator;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            return Result<LoginResponse>.Failure("Datos de entrada inválidos", errors);
        }

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return Result<LoginResponse>.Failure("Credenciales inválidas");
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Result<LoginResponse>.Failure("Credenciales inválidas");
        }

        var token = _jwtService.GenerateToken(user);

        var response = new LoginResponse(
            Token: token,
            User: new UserDto(
                Id: user.Id,
                Email: user.Email,
                FirstName: user.FirstName,
                LastName: user.LastName,
                FullName: user.FullName,
                Role: user.Role.ToString()
            )
        );

        return Result<LoginResponse>.Success(response, "Login exitoso");
    }
}