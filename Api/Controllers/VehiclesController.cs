using Application.MasterData.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehiclesController(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    /// <summary>
    /// Obtiene todos los vehículos
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAll()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        
        var vehicleDtos = vehicles.Select(v => new VehicleDto(
            v.Id,
            v.Plate,
            v.Brand,
            v.Model,
            v.Year,
            v.FuelCapacity,
            v.DriverId,
            v.Driver?.FullName
        ));

        return Ok(vehicleDtos);
    }

    /// <summary>
    /// Obtiene un vehículo por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto>> GetById(Guid id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        
        if (vehicle == null)
            return NotFound(new { message = "Vehículo no encontrado" });

        var vehicleDto = new VehicleDto(
            vehicle.Id,
            vehicle.Plate,
            vehicle.Brand,
            vehicle.Model,
            vehicle.Year,
            vehicle.FuelCapacity,
            vehicle.DriverId,
            vehicle.Driver?.FullName
        );

        return Ok(vehicleDto);
    }
}