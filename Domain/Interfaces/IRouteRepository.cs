using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Repositorio de Rutas (Solo Lectura)
/// </summary>
public interface IRouteRepository
{
    Task<IEnumerable<Route>> GetAllAsync();
    Task<Route?> GetByIdAsync(Guid id); 
}