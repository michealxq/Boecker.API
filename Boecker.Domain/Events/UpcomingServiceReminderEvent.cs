
using MediatR;

namespace Boecker.Domain.Events;

public class UpcomingServiceReminderEvent(int serviceScheduleId, DateTime dateScheduled, string clientEmail) : INotification
{
    public int ServiceScheduleId { get; } = serviceScheduleId;
    public DateTime DateScheduled { get; } = dateScheduled;
    public string ClientEmail { get; } = clientEmail;
}
