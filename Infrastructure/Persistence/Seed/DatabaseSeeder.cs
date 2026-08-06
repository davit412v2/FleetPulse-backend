using Application.Authentication.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Infrastructure.Persistence.Seed;

/// <summary>
/// Clase para poblar datos iniciales de usuarios
/// </summary>
public static class DatabaseSeeder
{
    public static async Task SeedAsync(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        // Verificar si ya hay usuarios
        var existingUsers = await userRepository.GetAllAsync();
        if (existingUsers.Any())
            return;

        var users = new List<User>
        {
            new User
            {
                Email = "admin@fp.com",
                PasswordHash = passwordHasher.HashPassword("Admin123"),
                FirstName = "Admin",
                LastName = "FleetPulse",
                Role = Role.Administrator,
                IsActive = true
            },
            new User
            {
                Email = "user@fp.com",
                PasswordHash = passwordHasher.HashPassword("User123"),
                FirstName = "Usuario",
                LastName = "Demo",
                Role = Role.User,
                IsActive = true
            }
        };

        foreach (var user in users)
        {
            await userRepository.AddAsync(user);
        }
    }
}