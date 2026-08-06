using Application.Telemetry.DTOs;

namespace Application.Telemetry.Interfaces;

/// <summary>
/// Servicio para transmitir telemetría en tiempo real
/// </summary>
public interface ITelemetryBroadcastService
{

    Task BroadcastTelemetryAsync(TelemetryDto telemetry);


    Task BroadcastTelemetryToVehicleGroupAsync(string vehicleId, TelemetryDto telemetry);

    Task BroadcastRecentTelemetryAsync(List<TelemetryDto> telemetryList);
}