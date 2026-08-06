using Application.Telemetry.DTOs;
using Application.Telemetry.Interfaces;
using Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

/// <summary>
/// Implementación del servicio de broadcast de telemetría
/// </summary>
public class TelemetryBroadcastService : ITelemetryBroadcastService
{
    private readonly IHubContext<TelemetryHub> _hubContext;
    private readonly ILogger<TelemetryBroadcastService> _logger;

    public TelemetryBroadcastService(
        IHubContext<TelemetryHub> hubContext,
        ILogger<TelemetryBroadcastService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task BroadcastTelemetryAsync(TelemetryDto telemetry)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTelemetry", telemetry);
            _logger.LogDebug("📡 Telemetría broadcast: {VehiclePlate}", telemetry.VehiclePlate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al transmitir telemetría");
        }
    }

    public async Task BroadcastTelemetryToVehicleGroupAsync(string vehicleId, TelemetryDto telemetry)
    {
        try
        {
            await _hubContext.Clients.Group($"Vehicle_{vehicleId}")
                .SendAsync("ReceiveTelemetry", telemetry);
            _logger.LogDebug("📡 Telemetría broadcast a grupo Vehicle_{VehicleId}", vehicleId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al transmitir telemetría al grupo");
        }
    }

    public async Task BroadcastRecentTelemetryAsync(List<TelemetryDto> telemetryList)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync("ReceiveRecentTelemetry", telemetryList);
            _logger.LogDebug("📡 Telemetría reciente broadcast: {Count} registros", telemetryList.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al transmitir telemetría reciente");
        }
    }
}