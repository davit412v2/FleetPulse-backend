using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio de rutas
/// </summary>
public class RouteRepository : IRouteRepository
{
    private readonly ApplicationDbContext _context;

    public RouteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Route>> GetAllAsync()
    {
        return await _context.Routes
            .Include(r => r.RoutePoints.OrderBy(rp => rp.Sequence))
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    public async Task<Route?> GetByIdAsync(Guid id)
    {
        return await _context.Routes
            .Include(r => r.RoutePoints.OrderBy(rp => rp.Sequence))
            .FirstOrDefaultAsync(r => r.Id == id && r.IsActive);
    }
}