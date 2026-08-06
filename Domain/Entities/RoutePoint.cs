namespace Domain.Entities;

/// <summary>
/// Entidad de Punto de Ruta (coordenadas GPS)
/// </summary>
public class RoutePoint : BaseEntity
{
    public Guid RouteId { get; set; }
    public Route Route { get; set; } = null!;

    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int Sequence { get; set; }
}