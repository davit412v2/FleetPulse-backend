namespace Domain.Entities;

/// <summary>
/// Entidad de Conductor
/// </summary>
public class Driver : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    // Navegación: Un conductor puede tener múltiples vehículos asignados
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}