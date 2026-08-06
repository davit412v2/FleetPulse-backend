using Microsoft.Extensions.Logging;
using Simulator.Models;

namespace Simulator.Services;

public class TelemetrySimulator : ITelemetrySimulator
{
    private readonly IApiService _apiService;
    private readonly ILogger<TelemetrySimulator> _logger;
    private readonly Random _random = new();
    private readonly Dictionary<Guid, VehicleSimulation> _vehicleSimulations = new();

    public TelemetrySimulator(IApiService apiService, ILogger<TelemetrySimulator> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("🚀 Iniciando simulador de telemetría...");

        // Cargar vehículos y rutas
        var vehicles = await _apiService.GetVehiclesAsync();
        var routes = await _apiService.GetRoutesAsync();

        if (!vehicles.Any() || !routes.Any())
        {
            _logger.LogError("❌ No hay vehículos o rutas disponibles");
            return;
        }

        // Seleccionar vehículos para simular (8-12 vehículos)
        var vehiclesToSimulate = vehicles.Take(_random.Next(8, Math.Min(13, vehicles.Count + 1))).ToList();
        _logger.LogInformation("🚗 Simulando {Count} vehículos", vehiclesToSimulate.Count);

        // Inicializar simulaciones
        foreach (var vehicle in vehiclesToSimulate)
        {
            var route = routes[_random.Next(routes.Count)];
            _vehicleSimulations[vehicle.Id] = new VehicleSimulation
            {
                Vehicle = vehicle,
                Route = route,
                CurrentPointIndex = 0,
                FuelLevel = vehicle.FuelCapacity * (decimal)(_random.NextDouble() * 0.5 + 0.5), // 50%-100%
                Temperature = (decimal)(_random.NextDouble() * 20 + 75) // 75-95°C
            };
        }

        // Loop principal de simulación
        while (!cancellationToken.IsCancellationRequested)
        {
            var tasks = _vehicleSimulations.Values.Select(sim => SimulateVehicleAsync(sim)).ToArray();
            await Task.WhenAll(tasks);

            _logger.LogInformation("📡 Telemetría enviada para {Count} vehículos", vehiclesToSimulate.Count);

            // Esperar 5-10 segundos entre actualizaciones
            await Task.Delay(_random.Next(5000, 10001), cancellationToken);
        }
    }

    private async Task SimulateVehicleAsync(VehicleSimulation sim)
    {
        try
        {
            // Obtener punto actual y siguiente
            var routePoints = sim.Route.RoutePoints.OrderBy(rp => rp.Sequence).ToList();
            var currentPoint = routePoints[sim.CurrentPointIndex];
            var nextIndex = (sim.CurrentPointIndex + 1) % routePoints.Count;
            var nextPoint = routePoints[nextIndex];

            // Calcular posición intermedia (movimiento gradual)
            var progress = (decimal)_random.NextDouble() * 0.3m; // Avanzar 0-30% hacia el siguiente punto
            var latitude = currentPoint.Latitude + (nextPoint.Latitude - currentPoint.Latitude) * progress;
            var longitude = currentPoint.Longitude + (nextPoint.Longitude - currentPoint.Longitude) * progress;

            // Generar datos realistas
            var speed = (decimal)(_random.NextDouble() * 80 + 20); // 20-100 km/h
            sim.FuelLevel -= (decimal)(_random.NextDouble() * 2 + 0.5); // Consumo 0.5-2.5L por actualización
            sim.Temperature += (decimal)(_random.NextDouble() * 4 - 2); // Variación ±2°C

            // Limites realistas
            if (sim.FuelLevel < 10) sim.FuelLevel = sim.Vehicle.FuelCapacity * 0.8m; // Reabastece
            if (sim.Temperature > 110) sim.Temperature = 95;
            if (sim.Temperature < 70) sim.Temperature = 75;

            // Crear telemetría
            var telemetry = new CreateTelemetryDto
            {
                VehicleId = sim.Vehicle.Id,
                RouteId = sim.Route.Id,
                Latitude = latitude,
                Longitude = longitude,
                Speed = speed,
                FuelLevel = sim.FuelLevel,
                Temperature = sim.Temperature,
                Timestamp = DateTime.UtcNow
            };

            // Enviar a API
            await _apiService.SendTelemetryAsync(telemetry);

            // Avanzar al siguiente punto ocasionalmente
            if (_random.NextDouble() > 0.7) // 30% de probabilidad de avanzar
            {
                sim.CurrentPointIndex = nextIndex;
            }

            _logger.LogDebug("📍 {Plate}: Lat={Lat:F5}, Lng={Lng:F5}, Speed={Speed:F1} km/h, Fuel={Fuel:F1}L",
                sim.Vehicle.Plate, latitude, longitude, speed, sim.FuelLevel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al simular vehículo {Plate}", sim.Vehicle.Plate);
        }
    }

    private class VehicleSimulation
    {
        public required VehicleDto Vehicle { get; init; }
        public required RouteDto Route { get; init; }
        public int CurrentPointIndex { get; set; }
        public decimal FuelLevel { get; set; }
        public decimal Temperature { get; set; }
    }
}