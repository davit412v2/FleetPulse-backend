using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Repositorio de Conductores
/// </summary>
public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync();
    Task<Driver?> GetByIdAsync(Guid id);
}