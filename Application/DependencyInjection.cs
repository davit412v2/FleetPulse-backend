using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Application.Authentication.Services;

namespace Application;

/// <summary>
/// Configuración de Dependency Injection para Application
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // FluentValidation
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Services
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}