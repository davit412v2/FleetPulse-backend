namespace Shared.Constants;

/// <summary>
/// Constantes de la aplicación
/// </summary>
public static class AppConstants
{
    public const int TelemetryUpdateIntervalSeconds = 2;
    public const int MaxHistoryDays = 7;
    
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string User = "User";
    }
    
    public static class VehicleStates
    {
        public const string Moving = "Moving";
        public const string Stopped = "Stopped";
        public const string Refueling = "Refueling";
        public const string Inactive = "Inactive";
    }
}