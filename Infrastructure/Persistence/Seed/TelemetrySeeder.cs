using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

/// <summary>
/// Seed de telemetría de prueba
/// </summary>
public static class TelemetrySeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Verificar si ya hay datos
        if (await context.Telemetry.AnyAsync())
            return;

        // Obtener vehículos y rutas existentes
        var vehicles = await context.Vehicles.Take(8).ToListAsync();
        var routes = await context.Routes.Take(5).ToListAsync();

        if (!vehicles.Any() || !routes.Any())
        {
            Console.WriteLine("⚠️ No hay vehículos o rutas para generar telemetría");
            return;
        }

        var telemetryList = new List<Telemetry>();
        var now = DateTime.UtcNow;
        var random = new Random();

        // Generar telemetría para cada vehículo (últimas 24 horas)
        foreach (var vehicle in vehicles)
        {
            var route = routes[random.Next(routes.Count)];

            // Generar 10-15 registros por vehículo en las últimas 24 horas
            var recordCount = random.Next(10, 16);

            for (int i = 0; i < recordCount; i++)
            {
                var hoursAgo = random.Next(0, 24);
                var minutesAgo = random.Next(0, 60);
                var timestamp = now.AddHours(-hoursAgo).AddMinutes(-minutesAgo);

                // Coordenadas base (Bogotá) con variación aleatoria
                var baseLatitude = 4.6097m + (decimal)(random.NextDouble() * 0.2 - 0.1);
                var baseLongitude = -74.0817m + (decimal)(random.NextDouble() * 0.2 - 0.1);

                // Velocidad: 0-100 km/h
                var speed = (decimal)(random.NextDouble() * 100);

                // Combustible: 20-150 litros (dependiendo de la capacidad del vehículo)
                var maxFuel = vehicle.FuelCapacity;
                var fuelLevel = (decimal)(random.NextDouble() * (double)maxFuel * 0.8 + (double)maxFuel * 0.2);

                // Temperatura: 70-110 °C
                var temperature = (decimal)(random.NextDouble() * 40 + 70);

                telemetryList.Add(new Telemetry
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicle.Id,
                    RouteId = random.Next(100) < 70 ? route.Id : null, // 70% tiene ruta asignada
                    Latitude = baseLatitude,
                    Longitude = baseLongitude,
                    Speed = speed,
                    FuelLevel = fuelLevel,
                    Temperature = temperature,
                    Timestamp = timestamp,
                    IsActive = true,
                    CreatedAt = timestamp
                });
            }
        }

        // Agregar telemetría MÁS reciente (últimos 10 minutos) para tener datos "en tiempo real"
        for (int i = 0; i < 5; i++)
        {
            var vehicle = vehicles[random.Next(vehicles.Count)];
            var route = routes[random.Next(routes.Count)];
            var minutesAgo = random.Next(0, 10);
            var timestamp = now.AddMinutes(-minutesAgo);

            var baseLatitude = 4.6097m + (decimal)(random.NextDouble() * 0.2 - 0.1);
            var baseLongitude = -74.0817m + (decimal)(random.NextDouble() * 0.2 - 0.1);

            telemetryList.Add(new Telemetry
            {
                Id = Guid.NewGuid(),
                VehicleId = vehicle.Id,
                RouteId = route.Id,
                Latitude = baseLatitude,
                Longitude = baseLongitude,
                Speed = (decimal)(random.NextDouble() * 80 + 20), // 20-100 km/h
                FuelLevel = (decimal)(random.NextDouble() * (double)vehicle.FuelCapacity * 0.6 + (double)vehicle.FuelCapacity * 0.3),
                Temperature = (decimal)(random.NextDouble() * 30 + 80), // 80-110 °C
                Timestamp = timestamp,
                IsActive = true,
                CreatedAt = timestamp
            });
        }

        await context.Telemetry.AddRangeAsync(telemetryList);
        await context.SaveChangesAsync();

        Console.WriteLine($"✅ Seed de telemetría completado: {telemetryList.Count} registros");
    }
}