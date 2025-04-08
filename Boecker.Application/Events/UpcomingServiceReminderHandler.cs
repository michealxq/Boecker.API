
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Events;

public class UpcomingServiceReminderHandler(
    IEmailService emailService,
    ILogger<UpcomingServiceReminderHandler> logger
) : INotificationHandler<UpcomingServiceReminderEvent>
{
    public async Task Handle(UpcomingServiceReminderEvent notification, CancellationToken cancellationToken)
    {
        var subject = "Upcoming Pest Control Service Reminder";
        var body = $"Dear customer, your pest control service is scheduled on {notification.DateScheduled:dddd, MMMM dd}. Please be prepared.";

        await emailService.SendEmailAsync(notification.ClientEmail, subject, body);

        logger.LogInformation("Sent upcoming service reminder for schedule #{ScheduleId}", notification.ServiceScheduleId);
    }
}
