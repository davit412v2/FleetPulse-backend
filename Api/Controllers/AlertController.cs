using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Interfaces;
using Domain.Entities;
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AlertController : ControllerBase
{
    private readonly IAlertRepository _alertRepository;
    private readonly ILogger<AlertController> _logger;

    public AlertController(
        IAlertRepository alertRepository,
        ILogger<AlertController> logger)
    {
        _alertRepository = alertRepository;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todas las alertas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Alert>>> GetAll()
    {
        var alerts = await _alertRepository.GetAllAsync();
        return Ok(alerts);
    }

    /// <summary>
    /// Obtener alertas por vehículo
    /// </summary>
    /// <returns></returns>
    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<List<Alert>>> GetAlertByVehicle(Guid vehicleId)
    {
        var alerts = await _alertRepository.GetByVehicleAsync(vehicleId);
        return Ok(alerts);
    }
}

