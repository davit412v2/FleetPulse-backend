namespace Simulator.Services;

public interface ITelemetrySimulator
{
    Task StartAsync(CancellationToken cancellationToken);
}