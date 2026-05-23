using VehicleManagementSystem.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace VehicleManagementSystem.Application.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string recipient, string subject, string message)
    {
        _logger.LogInformation("Notification → {Recipient} | {Subject}: {Message}", recipient, subject, message);
        return Task.CompletedTask;
    }
}
