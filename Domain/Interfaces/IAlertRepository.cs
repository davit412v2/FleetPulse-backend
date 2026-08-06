using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Repositorio para la gestión de alertas.
/// </summary>
public interface IAlertRepository
{
    Task AddAsync(Alert alert);

    Task<Alert?> GetByIdAsync(Guid id);

    Task<List<Alert>> GetAllAsync();

    Task<List<Alert>> GetUnreadAsync();

    Task<List<Alert>> GetByVehicleAsync(Guid vehicleId);

    Task MarkAsReadAsync(Guid id);

    Task DeleteOlderThanAsync(DateTime date);

    Task<int> SaveChangesAsync();
}