using Api.Service;
using Api.Services;
using Application.Alerts.DTOs;
using Application.Alerts.Services;
using Application.Telemetry.DTOs;
using Application.Telemetry.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TelemetryController : ControllerBase
{
    private readonly ITelemetryRepository _telemetryRepository;
    private readonly ITelemetryBroadcastService _broadcastService;
    private readonly ILogger<TelemetryController> _logger;

    private readonly IAlertRepository _alertRepository;
    private readonly IAlertRuleEngine _alertRuleEngine;
    private readonly IAlertBroadcastService _alertBroadcastService;

    public TelemetryController(
        ITelemetryRepository telemetryRepository,
        ITelemetryBroadcastService broadcastService,
        ILogger<TelemetryController> logger,
        IAlertRepository alertRepository,
        IAlertRuleEngine alertRuleEngine,
        IAlertBroadcastService alertBroadcastService
        )
    {
        _telemetryRepository = telemetryRepository;
        _broadcastService = broadcastService;
        _logger = logger;
        _alertRepository = alertRepository;
        _alertRuleEngine = alertRuleEngine;
        _alertBroadcastService = alertBroadcastService;
    }

    /// <summary>
    /// Crear un nuevo registro de telemetría
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TelemetryDto>> Create([FromBody] CreateTelemetryDto dto)
    {
        var telemetry = new Domain.Entities.Telemetry
        {
            Id = Guid.NewGuid(),
            VehicleId = dto.VehicleId,
            RouteId = dto.RouteId,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Speed = dto.Speed,
            FuelLevel = dto.FuelLevel,
            Temperature = dto.Temperature,
            Timestamp = dto.Timestamp ?? DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _telemetryRepository.AddAsync(telemetry);
        await _telemetryRepository.SaveChangesAsync();

        _logger.LogInformation("Telemetría creada para vehículo {VehicleId}", dto.VehicleId);

        var result = await _telemetryRepository.GetByIdAsync(telemetry.Id);


        if (result == null)
            return NotFound();

        await AlertService.GeneratedAlert(_alertRuleEngine, _alertRepository, _alertBroadcastService, result);

        var telemetryDto = MapToDto(result);

        await _broadcastService.BroadcastTelemetryAsync(telemetryDto);
        await _broadcastService.BroadcastTelemetryToVehicleGroupAsync(dto.VehicleId.ToString(), telemetryDto);

        return Ok(telemetryDto);
    }

    /// <summary>
    /// Obtener telemetría por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TelemetryDto>> GetById(Guid id)
    {
        var telemetry = await _telemetryRepository.GetByIdAsync(id);

        if (telemetry == null)
            return NotFound();

        return Ok(MapToDto(telemetry));
    }

    /// <summary>
    /// Obtener telemetría reciente (últimas 24 horas por defecto)
    /// </summary>
    [HttpGet("recent")]
    public async Task<ActionResult<List<TelemetryDto>>> GetRecent([FromQuery] int hours = 24)
    {
        var telemetryList = await _telemetryRepository.GetRecentAsync(hours);
        return Ok(telemetryList.Select(MapToDto).ToList());
    }

    /// <summary>
    /// Obtener telemetría de un vehículo específico
    /// </summary>
    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<List<TelemetryDto>>> GetByVehicle(Guid vehicleId, [FromQuery] int limit = 100)
    {
        var telemetryList = await _telemetryRepository.GetByVehicleIdAsync(vehicleId, limit);
        return Ok(telemetryList.Select(MapToDto).ToList());
    }

    /// <summary>
    /// Obtener telemetría de un vehículo en un rango de fechas
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/range")]
    public async Task<ActionResult<List<TelemetryDto>>> GetByVehicleAndDateRange(
        Guid vehicleId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var telemetryList = await _telemetryRepository.GetByVehicleAndDateRangeAsync(vehicleId, startDate, endDate);
        return Ok(telemetryList.Select(MapToDto).ToList());
    }

    // Helper method para mapear Entity → DTO
    private static TelemetryDto MapToDto(Domain.Entities.Telemetry telemetry)
    {
        return new TelemetryDto
        {
            Id = telemetry.Id,
            VehicleId = telemetry.VehicleId,
            VehiclePlate = telemetry.Vehicle?.Plate ?? string.Empty,
            RouteId = telemetry.RouteId,
            RouteName = telemetry.Route?.Name,
            Latitude = telemetry.Latitude,
            Longitude = telemetry.Longitude,
            Speed = telemetry.Speed,
            FuelLevel = telemetry.FuelLevel,
            Temperature = telemetry.Temperature,
            Timestamp = telemetry.Timestamp
        };
    }
}