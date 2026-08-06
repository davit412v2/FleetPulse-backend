using Api.Hubs;
using Application.Alerts.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

/// <summary>
/// Servicio encargado de transmitir alertas a los clientes mediante SignalR.
/// </summary>
public class AlertBroadcastService : IAlertBroadcastService
{
    private readonly IHubContext<TelemetryHub> _hubContext;
    private readonly ILogger<AlertBroadcastService> _logger;

    public AlertBroadcastService(
        IHubContext<TelemetryHub> hubContext,
        ILogger<AlertBroadcastService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task BroadcastAlertAsync(AlertDto alert)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync(
                "ReceiveAlert",
                alert);

            _logger.LogInformation(
                "🚨 Alerta enviada: {Title} - {Vehicle}",
                alert.Title,
                alert.VehiclePlate);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "❌ Error enviando alerta.");
        }
    }


    public async Task BroadcastAlertToVehicleGroupAsync(
        string vehicleId,
        AlertDto alert)
    {
        try
        {
            await _hubContext.Clients
                .Group($"Vehicle_{vehicleId}")
                .SendAsync(
                    "ReceiveAlert",
                    alert);

            _logger.LogInformation(
                "🚨 Alerta enviada al grupo Vehicle_{VehicleId}",
                vehicleId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "❌ Error enviando alerta al grupo.");
        }
    }


    public async Task BroadcastAlertsAsync(List<AlertDto> alerts)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync(
                "ReceiveAlerts",
                alerts);

            _logger.LogInformation(
                "🚨 {Count} alertas enviadas.",
                alerts.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "❌ Error enviando listado de alertas.");
        }
    }
}