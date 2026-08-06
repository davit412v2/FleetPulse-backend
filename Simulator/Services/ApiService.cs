using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Simulator.Models;

namespace Simulator.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;

    private readonly ITokenStore _tokenStore;

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger, ITokenStore tokenStore)
    {
        _httpClient = httpClient;
        _logger = logger;
        _tokenStore = tokenStore;
    }

    private void ConfigureAuthorization()
    {
        if (!_tokenStore ?.Token?.Any() ?? true)
            throw new InvalidOperationException("No existe un token válido.");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _tokenStore!.Token);
    }


    public async Task<List<VehicleDto>> GetVehiclesAsync()
    {
        try
        {
            ConfigureAuthorization();
            var vehicles = await _httpClient.GetFromJsonAsync<List<VehicleDto>>("/api/Vehicles");

            return vehicles ?? new List<VehicleDto>();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al obtener vehículos");
            throw;
        }
    }

    public async Task<List<RouteDto>> GetRoutesAsync()
    {
        try
        {

            ConfigureAuthorization();
            var routes = await _httpClient.GetFromJsonAsync<List<RouteDto>>("/api/Routes");
            return routes ?? new List<RouteDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al obtener rutas");
            throw;
        }
    }

    public async Task SendTelemetryAsync(CreateTelemetryDto telemetry)
    {
        try
        {
            ConfigureAuthorization();
            var response = await _httpClient.PostAsJsonAsync("/api/Telemetry", telemetry);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al enviar telemetría");
            throw;
        }
    }
}