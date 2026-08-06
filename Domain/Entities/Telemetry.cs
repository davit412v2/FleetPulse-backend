namespace Domain.Entities;

/// <summary>
/// Registro de telemetría de un vehículo en un momento específico
/// </summary>
public class Telemetry : BaseEntity
{
    public Guid VehicleId { get; set; }

    public Guid? RouteId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public decimal Speed { get; set; }

    public decimal FuelLevel { get; set; }

    public decimal Temperature { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual Route? Route { get; set; }
}