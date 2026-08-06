using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Interface para operaciones de telemetría
/// </summary>
public interface ITelemetryRepository
{
    /// <summary>
    /// Agregar un nuevo registro de telemetría
    /// </summary>
    Task AddAsync(Telemetry telemetry);

    /// <summary>
    /// Obtener telemetría por ID
    /// </summary>
    Task<Telemetry?> GetByIdAsync(Guid id);

    /// <summary>
    /// Obtener telemetría de un vehículo específico
    /// </summary>
    /// <param name="vehicleId">ID del vehículo</param>
    /// <param name="limit">Límite de registros (últimos N)</param>
    Task<List<Telemetry>> GetByVehicleIdAsync(Guid vehicleId, int limit = 100);

    /// <summary>
    /// Obtener telemetría de un vehículo en un rango de fechas
    /// </summary>
    Task<List<Telemetry>> GetByVehicleAndDateRangeAsync(Guid vehicleId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Obtener toda la telemetría reciente (últimas N horas)
    /// </summary>
    Task<List<Telemetry>> GetRecentAsync(int hours = 24);

    /// <summary>
    /// Eliminar registros anteriores a una fecha específica
    /// </summary>
    Task DeleteOlderThanAsync(DateTime date);

    /// <summary>
    /// Guardar cambios
    /// </summary>
    Task<int> SaveChangesAsync();
}