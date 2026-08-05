using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Entidad de usuario
/// </summary>
public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;

    public string FullName => $"{FirstName} {LastName}";
}