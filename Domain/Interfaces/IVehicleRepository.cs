using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Repositorio de Vehículos (Solo Lectura)
/// </summary>
public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(Guid id);
}