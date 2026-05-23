namespace VehicleManagementSystem.Core.Interfaces;

public interface INotificationService
{
    Task SendAsync(string recipient, string subject, string message);
}
