using Api.Services;
using Application.Alerts.DTOs;
using Application.Alerts.Services;
using Domain.Entities;
using Domain.Interfaces;

namespace Api.Service;


public class AlertService
{
    public static async Task GeneratedAlert(IAlertRuleEngine _alertRuleEngine, IAlertRepository _alertRepository, IAlertBroadcastService _alertBroadcastService, Domain.Entities.Telemetry result)
    {
        var alerts = _alertRuleEngine.Evaluate(result);
        foreach (var alert in alerts)
        {
            await _alertRepository.AddAsync(alert);
        }

        if (alerts.Any())
        {
            await _alertRepository.SaveChangesAsync();
        }

        foreach (var alert in alerts)
        {
            var dtoAlert = MapToDtoAlert(alert);

            await _alertBroadcastService.BroadcastAlertAsync(dtoAlert);

            await _alertBroadcastService.BroadcastAlertToVehicleGroupAsync(
                dtoAlert.VehicleId.ToString(),
                dtoAlert);
        }


    }

    private static AlertDto MapToDtoAlert(Alert alert)
    {
        return new AlertDto
        {
            Id = alert.Id,
            VehicleId = alert.VehicleId,
            VehiclePlate = alert.Vehicle?.Plate ?? string.Empty,
            TelemetryId = alert.TelemetryId,
            Type = alert.Type,
            Severity = alert.Severity,
            Title = alert.Title,
            Message = alert.Message,
            Value = alert.Value,
            IsRead = alert.IsRead,
            Timestamp = alert.Timestamp
        };
    }
}