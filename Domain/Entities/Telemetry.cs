namespace Domain.Entities;

/// <summary>
/// Registro de telemetría de un vehículo en un momento específico
/// </summary>
public class Telemetry : BaseEntity
{
    /// <summary>
    /// ID del vehículo que reporta la telemetría
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// ID de la ruta que está siguiendo (opcional)
    /// </summary>
    public Guid? RouteId { get; set; }

    /// <summary>
    /// Latitud GPS (decimal degrees)
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitud GPS (decimal degrees)
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Velocidad en km/h
    /// </summary>
    public decimal Speed { get; set; }

    /// <summary>
    /// Nivel de combustible en litros
    /// </summary>
    public decimal FuelLevel { get; set; }

    /// <summary>
    /// Temperatura del motor en °C
    /// </summary>
    public decimal Temperature { get; set; }

    /// <summary>
    /// Timestamp del registro (UTC)
    /// </summary>
    public DateTime Timestamp { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual Route? Route { get; set; }
}