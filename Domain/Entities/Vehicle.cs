namespace Domain.Entities;

/// <summary>
/// Entidad de Vehículo
/// </summary>
public class Vehicle : BaseEntity
{
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal FuelCapacity { get; set; } // Capacidad en litros

    // Relación con Driver
    public Guid? DriverId { get; set; }
    public Driver? Driver { get; set; }
}