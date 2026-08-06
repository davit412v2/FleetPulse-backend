using Application.MasterData.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoutesController : ControllerBase
{
    private readonly IRouteRepository _routeRepository;

    public RoutesController(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }

    /// <summary>
    /// Obtiene todas las rutas con sus puntos GPS
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RouteDto>>> GetAll()
    {
        var routes = await _routeRepository.GetAllAsync();
        
        var routeDtos = routes.Select(r => new RouteDto(
            r.Id,
            r.Name,
            r.Origin,
            r.Destination,
            r.Distance,
            r.EstimatedTimeMinutes,
            r.RoutePoints.Select(rp => new RoutePointDto(
                rp.Latitude,
                rp.Longitude,
                rp.Sequence
            )).ToList()
        ));

        return Ok(routeDtos);
    }

    /// <summary>
    /// Obtiene una ruta por ID con sus puntos GPS
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RouteDto>> GetById(Guid id)
    {
        var route = await _routeRepository.GetByIdAsync(id);
        
        if (route == null)
            return NotFound(new { message = "Ruta no encontrada" });

        var routeDto = new RouteDto(
            route.Id,
            route.Name,
            route.Origin,
            route.Destination,
            route.Distance,
            route.EstimatedTimeMinutes,
            route.RoutePoints.Select(rp => new RoutePointDto(
                rp.Latitude,
                rp.Longitude,
                rp.Sequence
            )).ToList()
        );

        return Ok(routeDto);
    }
}