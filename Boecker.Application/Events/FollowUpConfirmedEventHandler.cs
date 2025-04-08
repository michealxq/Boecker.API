
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Events;

public class FollowUpConfirmedEventHandler(
    IEmailService emailService,
    ILogger<FollowUpConfirmedEventHandler> logger
) : INotificationHandler<FollowUpConfirmedEvent>
{
    public async Task Handle(FollowUpConfirmedEvent notification, CancellationToken cancellationToken)
    {
        var subject = "✅ Follow-up Service Confirmed";
        var body = $"Your follow-up service is scheduled for {notification.ScheduledDate:MMMM dd, yyyy}.\n" +
                   $"Contract ID: {notification.ContractId}";

        await emailService.SendEmailAsync(notification.ClientEmail, subject, body);
        logger.LogInformation("📬 Follow-up confirmation email sent to {Email}", notification.ClientEmail);
    }
}