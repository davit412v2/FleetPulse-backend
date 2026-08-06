using Application.Telemetry.DTOs;

namespace Application.Telemetry.Interfaces;

/// <summary>
/// Servicio para transmitir telemetría en tiempo real
/// </summary>
public interface ITelemetryBroadcastService
{
    /// <summary>
    /// Transmitir telemetría a todos los clientes conectados
    /// </summary>
    Task BroadcastTelemetryAsync(TelemetryDto telemetry);

    /// <summary>
    /// Transmitir telemetría a clientes suscritos a un vehículo específico
    /// </summary>
    Task BroadcastTelemetryToVehicleGroupAsync(string vehicleId, TelemetryDto telemetry);

    /// <summary>
    /// Transmitir lista de telemetría reciente
    /// </summary>
    Task BroadcastRecentTelemetryAsync(List<TelemetryDto> telemetryList);
}