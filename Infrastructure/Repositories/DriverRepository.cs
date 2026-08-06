using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio de conductores
/// </summary>
public class DriverRepository : IDriverRepository
{
    private readonly ApplicationDbContext _context;

    public DriverRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver>> GetAllAsync()
    {
        return await _context.Drivers
            .Where(d => d.IsActive)
            .OrderBy(d => d.FirstName)
            .ToListAsync();
    }

    public async Task<Driver?> GetByIdAsync(Guid id)
    {
        return await _context.Drivers
            .Include(d => d.Vehicles)
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
    }
}