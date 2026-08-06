using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

/// <summary>
/// Seed de datos maestros (Drivers, Vehicles, Routes)
/// </summary>
public static class MasterDataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Verificar si ya hay datos COMPLETOS
        var hasDrivers = await context.Drivers.AnyAsync();
        var hasVehicles = await context.Vehicles.AnyAsync();
        var hasRoutes = await context.Routes.AnyAsync();

        // Si YA están todos los datos, no hacer nada
        if (hasDrivers && hasVehicles && hasRoutes)
            return;

        var now = DateTime.UtcNow;

        // Insertar Drivers solo si no existen
        if (!hasDrivers)
        {
            var drivers = GetDrivers(now);
            await context.Drivers.AddRangeAsync(drivers);
            await context.SaveChangesAsync();
        }

        // Obtener drivers existentes (para FK de Vehicles)
        var existingDrivers = await context.Drivers.ToListAsync();

        // Insertar Vehicles solo si no existen
        if (!hasVehicles && existingDrivers.Any())
        {
            var vehicles = GetVehicles(existingDrivers, now);
            await context.Vehicles.AddRangeAsync(vehicles);
            await context.SaveChangesAsync();
        }

        // Insertar Routes solo si no existen
        if (!hasRoutes)
        {
            var routes = GetRoutes(now);
            await context.Routes.AddRangeAsync(routes);
            await context.SaveChangesAsync();
        }
    }

    private static List<Driver> GetDrivers(DateTime now)
    {
        return new List<Driver>
    {
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000001"), FirstName = "Carlos", LastName = "Rodríguez", LicenseNumber = "C-12345678", Phone = "3001234567", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000002"), FirstName = "María", LastName = "González", LicenseNumber = "C-23456789", Phone = "3012345678", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000003"), FirstName = "Juan", LastName = "Martínez", LicenseNumber = "C-34567890", Phone = "3023456789", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000004"), FirstName = "Ana", LastName = "López", LicenseNumber = "C-45678901", Phone = "3034567890", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000005"), FirstName = "Luis", LastName = "Hernández", LicenseNumber = "C-56789012", Phone = "3045678901", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000006"), FirstName = "Diana", LastName = "Pérez", LicenseNumber = "C-67890123", Phone = "3056789012", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000007"), FirstName = "Jorge", LastName = "Ramírez", LicenseNumber = "C-78901234", Phone = "3067890123", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000008"), FirstName = "Sandra", LastName = "Torres", LicenseNumber = "C-89012345", Phone = "3078901234", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-000000000009"), FirstName = "Miguel", LastName = "Vargas", LicenseNumber = "C-90123456", Phone = "3089012345", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-00000000000a"), FirstName = "Laura", LastName = "Morales", LicenseNumber = "C-01234567", Phone = "3090123456", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-00000000000b"), FirstName = "Pedro", LastName = "Castro", LicenseNumber = "C-11111111", Phone = "3101112233", IsActive = true, CreatedAt = now },
        new Driver { Id = Guid.Parse("11111111-0000-0000-0000-00000000000c"), FirstName = "Claudia", LastName = "Ruiz", LicenseNumber = "C-22222222", Phone = "3202223344", IsActive = true, CreatedAt = now }
    };
    }

    private static List<Vehicle> GetVehicles(List<Driver> drivers, DateTime now)
    {
        return new List<Vehicle>
    {
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000001"), Plate = "ABC123", Brand = "Chevrolet", Model = "NHR", Year = 2020, FuelCapacity = 80, DriverId = drivers[0].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000002"), Plate = "DEF456", Brand = "Hino", Model = "Serie 300", Year = 2021, FuelCapacity = 100, DriverId = drivers[1].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000003"), Plate = "GHI789", Brand = "Isuzu", Model = "NQR", Year = 2019, FuelCapacity = 90, DriverId = drivers[2].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000004"), Plate = "JKL012", Brand = "Chevrolet", Model = "FRR", Year = 2022, FuelCapacity = 110, DriverId = drivers[3].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000005"), Plate = "MNO345", Brand = "Hino", Model = "Serie 500", Year = 2020, FuelCapacity = 120, DriverId = drivers[4].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000006"), Plate = "PQR678", Brand = "Mercedes-Benz", Model = "Atego", Year = 2021, FuelCapacity = 100, DriverId = drivers[5].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000007"), Plate = "STU901", Brand = "Volvo", Model = "VM", Year = 2019, FuelCapacity = 130, DriverId = drivers[6].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000008"), Plate = "VWX234", Brand = "Isuzu", Model = "NPR", Year = 2022, FuelCapacity = 85, DriverId = drivers[7].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-000000000009"), Plate = "YZA567", Brand = "Chevrolet", Model = "NKR", Year = 2020, FuelCapacity = 75, DriverId = drivers[8].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-00000000000a"), Plate = "BCD890", Brand = "Hino", Model = "Serie 700", Year = 2021, FuelCapacity = 140, DriverId = drivers[9].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-00000000000b"), Plate = "EFG123", Brand = "Ford", Model = "Cargo", Year = 2019, FuelCapacity = 95, DriverId = drivers[10].Id, IsActive = true, CreatedAt = now },
        new Vehicle { Id = Guid.Parse("22222222-0000-0000-0000-00000000000c"), Plate = "HIJ456", Brand = "Kenworth", Model = "T800", Year = 2022, FuelCapacity = 150, DriverId = drivers[11].Id, IsActive = true, CreatedAt = now }
    };
    }
    private static List<Route> GetRoutes(DateTime now)
    {
        return new List<Route>
    {
        // Ruta 1: Bogotá - Chía
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000001"),
            Name = "Bogotá Centro - Chía",
            Origin = "Bogotá Centro",
            Destination = "Chía, Cundinamarca",
            Distance = 25.5m,
            EstimatedTimeMinutes = 45,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.7110000m, Longitude = -74.0721000m, Sequence = 2, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.8640000m, Longitude = -74.0580000m, Sequence = 3, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 2: Bogotá - Soacha
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000002"),
            Name = "Bogotá Sur - Soacha",
            Origin = "Bogotá Sur",
            Destination = "Soacha, Cundinamarca",
            Distance = 18.2m,
            EstimatedTimeMinutes = 35,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.5773000m, Longitude = -74.2160000m, Sequence = 2, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 3: Bogotá - Zipaquirá
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000003"),
            Name = "Bogotá - Zipaquirá",
            Origin = "Bogotá Norte",
            Destination = "Zipaquirá, Cundinamarca",
            Distance = 48.7m,
            EstimatedTimeMinutes = 70,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.8640000m, Longitude = -74.0580000m, Sequence = 2, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 5.0220000m, Longitude = -73.9947000m, Sequence = 3, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 4: Bogotá - Facatativá
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000004"),
            Name = "Bogotá - Facatativá",
            Origin = "Bogotá Occidente",
            Destination = "Facatativá, Cundinamarca",
            Distance = 40.3m,
            EstimatedTimeMinutes = 55,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.8145000m, Longitude = -74.3549000m, Sequence = 2, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 5: Bogotá - La Calera
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000005"),
            Name = "Bogotá - La Calera",
            Origin = "Bogotá Oriente",
            Destination = "La Calera, Cundinamarca",
            Distance = 22.1m,
            EstimatedTimeMinutes = 40,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.7244000m, Longitude = -73.9686000m, Sequence = 2, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 6: Bogotá - Mosquera
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000006"),
            Name = "Bogotá - Mosquera",
            Origin = "Bogotá Fontibón",
            Destination = "Mosquera, Cundinamarca",
            Distance = 16.5m,
            EstimatedTimeMinutes = 30,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.7057000m, Longitude = -74.2325000m, Sequence = 2, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 7: Bogotá - Funza
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000007"),
            Name = "Bogotá - Funza",
            Origin = "Bogotá Calle 80",
            Destination = "Funza, Cundinamarca",
            Distance = 19.8m,
            EstimatedTimeMinutes = 35,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.7166000m, Longitude = -74.2103000m, Sequence = 2, IsActive = true, CreatedAt = now }
            }
        },
        // Ruta 8: Bogotá - Madrid
        new Route
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000008"),
            Name = "Bogotá - Madrid",
            Origin = "Bogotá Calle 13",
            Destination = "Madrid, Cundinamarca",
            Distance = 32.4m,
            EstimatedTimeMinutes = 50,
            IsActive = true,
            CreatedAt = now,
            RoutePoints = new List<RoutePoint>
            {
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.6097100m, Longitude = -74.0817500m, Sequence = 1, IsActive = true, CreatedAt = now },
                new RoutePoint { Id = Guid.NewGuid(), Latitude = 4.7316000m, Longitude = -74.2646000m, Sequence = 2, IsActive = true, CreatedAt = now }
            }
        }
    };
    }
}