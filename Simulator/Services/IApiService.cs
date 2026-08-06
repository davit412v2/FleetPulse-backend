using Simulator.Models;

namespace Simulator.Services;

public interface IApiService
{
    Task<List<VehicleDto>> GetVehiclesAsync();
    Task<List<RouteDto>> GetRoutesAsync();
    Task SendTelemetryAsync(CreateTelemetryDto telemetry);
}