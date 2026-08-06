using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio de Alertas.
/// </summary>
public class AlertRepository : IAlertRepository
{
    private readonly ApplicationDbContext _context;

    public AlertRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Alert alert)
    {
        await _context.Alerts.AddAsync(alert);
    }

    public async Task<Alert?> GetByIdAsync(Guid id)
    {
        return await _context.Alerts
            .Include(x => x.Vehicle)
            .Include(x => x.Telemetry)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Alert>> GetAllAsync()
    {
        return await _context.Alerts
            .Include(x => x.Vehicle)
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task<List<Alert>> GetUnreadAsync()
    {
        return await _context.Alerts
            .Include(x => x.Vehicle)
            .Where(x => !x.IsRead)
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task<List<Alert>> GetByVehicleAsync(Guid vehicleId)
    {
        return await _context.Alerts
            .Include(x => x.Vehicle)
            .Where(x => x.VehicleId == vehicleId)
            .OrderByDescending(x => x.Timestamp)
            .Take(10)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var alert = await _context.Alerts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (alert == null)
            return;

        alert.IsRead = true;
    }

    public async Task DeleteOlderThanAsync(DateTime date)
    {
        var alerts = await _context.Alerts
            .Where(x => x.Timestamp < date)
            .ToListAsync();

        _context.Alerts.RemoveRange(alerts);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}