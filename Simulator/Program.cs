using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simulator.Models;
using Simulator.Services;

var builder = Host.CreateApplicationBuilder(args);

// Configuración
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Servicios
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] 
    ?? throw new InvalidOperationException("ApiSettings:BaseUrl no configurado");

builder.Services.AddSingleton<ITokenStore, TokenStore>(); 

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});


builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});


builder.Services.AddScoped<ITelemetrySimulator, TelemetrySimulator>();

var app = builder.Build();

// Ejecutar simulador
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    var apiService = services.GetRequiredService<IApiService>();
    var simulator = services.GetRequiredService<ITelemetrySimulator>();
    var auth = services.GetRequiredService<IAuthenticationService>();


    logger.LogInformation("🚀 FleetPulse Simulator v1.0");
    logger.LogInformation("📡 API: {BaseUrl}", apiBaseUrl);

    // Login
    var email = builder.Configuration["ApiSettings:Email"] ?? "admin@fp.com";
    var password = builder.Configuration["ApiSettings:Password"] ?? "Admin123";
    

    await auth.LoginAsync(email, password);

    // Iniciar simulación
    var cts = new CancellationTokenSource();
    Console.CancelKeyPress += (s, e) =>
    {
        logger.LogInformation("🛑 Deteniendo simulador...");
        cts.Cancel();
        e.Cancel = true;
    };

    await simulator.StartAsync(cts.Token);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "❌ Error crítico en el simulador");
    return 1;
}

return 0;