using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

/// <summary>
/// Servicio en background para limpiar telemetría antigua (> 7 días)
/// </summary>
public class TelemetryCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TelemetryCleanupService> _logger;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(24); 
    private readonly int _retentionDays = 7;

    public TelemetryCleanupService(IServiceProvider serviceProvider, ILogger<TelemetryCleanupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🧹 TelemetryCleanupService iniciado. Limpieza cada {Interval} horas, retención de {Days} días",
            _cleanupInterval.TotalHours, _retentionDays);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanupOldTelemetryAsync();
                await Task.Delay(_cleanupInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("TelemetryCleanupService detenido");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error en TelemetryCleanupService");
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }

    private async Task CleanupOldTelemetryAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var telemetryRepository = scope.ServiceProvider.GetRequiredService<ITelemetryRepository>();

        var cutoffDate = DateTime.UtcNow.AddDays(-_retentionDays);

        _logger.LogInformation("🧹 Iniciando limpieza de telemetría anterior a {CutoffDate}", cutoffDate);

        await telemetryRepository.DeleteOlderThanAsync(cutoffDate);
        var deletedCount = await telemetryRepository.SaveChangesAsync();

        _logger.LogInformation("✅ Limpieza completada. Registros eliminados: {Count}", deletedCount);
    }
}