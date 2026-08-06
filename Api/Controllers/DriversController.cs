using Application.MasterData.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DriversController : ControllerBase
{
    private readonly IDriverRepository _driverRepository;

    public DriversController(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    /// <summary>
    /// Obtiene todos los conductores
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll()
    {
        var drivers = await _driverRepository.GetAllAsync();
        
        var driverDtos = drivers.Select(d => new DriverDto(
            d.Id,
            d.FirstName,
            d.LastName,
            d.FullName,
            d.LicenseNumber,
            d.Phone
        ));

        return Ok(driverDtos);
    }

    /// <summary>
    /// Obtiene un conductor por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> GetById(Guid id)
    {
        var driver = await _driverRepository.GetByIdAsync(id);
        
        if (driver == null)
            return NotFound(new { message = "Conductor no encontrado" });

        var driverDto = new DriverDto(
            driver.Id,
            driver.FirstName,
            driver.LastName,
            driver.FullName,
            driver.LicenseNumber,
            driver.Phone
        );

        return Ok(driverDto);
    }
}