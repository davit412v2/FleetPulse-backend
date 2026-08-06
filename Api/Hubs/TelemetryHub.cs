using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

/// <summary>
/// Hub de SignalR para transmitir telemetría en tiempo real
/// </summary>
[Authorize]
public class TelemetryHub : Hub
{
    private readonly ILogger<TelemetryHub> _logger;

    public TelemetryHub(ILogger<TelemetryHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.Identity?.Name ?? "Anonymous";
        _logger.LogInformation("🔌 Cliente conectado: {ConnectionId} (Usuario: {UserId})", Context.ConnectionId, userId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("🔌 Cliente desconectado: {ConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Permite al cliente unirse a un grupo de vehículo específico
    /// </summary>
    public async Task JoinVehicleGroup(string vehicleId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Vehicle_{vehicleId}");
        _logger.LogInformation("👥 Cliente {ConnectionId} se unió al grupo Vehicle_{VehicleId}", Context.ConnectionId, vehicleId);
    }

    /// <summary>
    /// Permite al cliente salir de un grupo de vehículo específico
    /// </summary>
    public async Task LeaveVehicleGroup(string vehicleId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Vehicle_{vehicleId}");
        _logger.LogInformation("👥 Cliente {ConnectionId} salió del grupo Vehicle_{VehicleId}", Context.ConnectionId, vehicleId);
    }
}