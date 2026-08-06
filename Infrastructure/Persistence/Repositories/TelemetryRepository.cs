using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio de telemetría
/// </summary>
public class TelemetryRepository : ITelemetryRepository
{
    private readonly ApplicationDbContext _context;

    public TelemetryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Telemetry telemetry)
    {
        await _context.Telemetry.AddAsync(telemetry);
    }

    public async Task<Telemetry?> GetByIdAsync(Guid id)
    {
        return await _context.Telemetry
            .Include(t => t.Vehicle)
            .Include(t => t.Route)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Telemetry>> GetByVehicleIdAsync(Guid vehicleId, int limit = 100)
    {
        return await _context.Telemetry
            .Include(t => t.Vehicle)
            .Include(t => t.Route)
            .Where(t => t.VehicleId == vehicleId)
            .OrderByDescending(t => t.Timestamp)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<Telemetry>> GetByVehicleAndDateRangeAsync(Guid vehicleId, DateTime startDate, DateTime endDate)
    {
        return await _context.Telemetry
            .Include(t => t.Vehicle)
            .Include(t => t.Route)
            .Where(t => t.VehicleId == vehicleId && t.Timestamp >= startDate && t.Timestamp <= endDate)
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task<List<Telemetry>> GetRecentAsync(int hours = 24)
    {
        var cutoffDate = DateTime.UtcNow.AddHours(-hours);

        return await _context.Telemetry
            .Include(t => t.Vehicle)
            .Include(t => t.Route)
            .Where(t => t.Timestamp >= cutoffDate)
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task DeleteOlderThanAsync(DateTime date)
    {
        var oldRecords = await _context.Telemetry
            .Where(t => t.Timestamp < date)
            .ToListAsync();

        _context.Telemetry.RemoveRange(oldRecords);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}