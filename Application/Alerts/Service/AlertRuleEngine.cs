using Domain.Entities;
using Domain.Enums;
using TelemetryEntity = Domain.Entities.Telemetry;

namespace Application.Alerts.Services;

public class AlertRuleEngine : IAlertRuleEngine
{
    public List<Alert> Evaluate(TelemetryEntity telemetry )
    {
        var alerts = new List<Alert>();

        alerts.AddRange(CheckLowFuel(telemetry));
        alerts.AddRange(CheckHighTemperature(telemetry));

        return alerts;
    }

    private IEnumerable<Alert> CheckLowFuel(TelemetryEntity telemetry)
    {
        if (telemetry.FuelLevel > 10)
            yield break;

        yield return new Alert
        {
            Id = Guid.NewGuid(),
            VehicleId = telemetry.VehicleId,
            TelemetryId = telemetry.Id,
            Type = AlertType.LowFuel,
            Severity = AlertSeverity.Critical,
            Title = "Combustible Bajo",
            Message = $"Nivel crítico de combustible ({telemetry.FuelLevel:F1} L).",
            Value = telemetry.FuelLevel,
            Timestamp = telemetry.Timestamp,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    private IEnumerable<Alert> CheckHighTemperature(TelemetryEntity telemetry)
    {
        if (telemetry.Temperature < 95)
            yield break;

        yield return new Alert
        {
            Id = Guid.NewGuid(),
            VehicleId = telemetry.VehicleId,
            TelemetryId = telemetry.Id,
            Type = AlertType.HighTemperature,
            Severity = AlertSeverity.Warning,
            Title = "Temperatura Alta",
            Message = $"Temperatura del motor {telemetry.Temperature:F1} °C.",
            Value = telemetry.Temperature,
            Timestamp = telemetry.Timestamp,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

}