using Domain.Entities;
using TelemetryEntity = Domain.Entities.Telemetry;

namespace Application.Alerts.Services;

public interface IAlertRuleEngine
{
    List<Alert> Evaluate(TelemetryEntity telemetry);
}