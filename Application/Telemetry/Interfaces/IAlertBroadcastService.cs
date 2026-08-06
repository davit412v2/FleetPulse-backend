using Application.Alerts.DTOs;

namespace Api.Services;

/// <summary>
/// Servicio encargado de transmitir alertas mediante SignalR.
/// </summary>
public interface IAlertBroadcastService
{

    Task BroadcastAlertAsync(AlertDto alert);

    Task BroadcastAlertToVehicleGroupAsync(string vehicleId, AlertDto alert);

    Task BroadcastAlertsAsync(List<AlertDto> alerts);
}