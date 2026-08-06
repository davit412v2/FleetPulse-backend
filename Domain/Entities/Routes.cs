namespace Domain.Entities;

/// <summary>
/// Entidad de Ruta
/// </summary>
public class Route : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public decimal Distance { get; set; } // Distancia en kilómetros
    public int EstimatedTimeMinutes { get; set; } 
    public ICollection<RoutePoint> RoutePoints { get; set; } = new List<RoutePoint>();
}