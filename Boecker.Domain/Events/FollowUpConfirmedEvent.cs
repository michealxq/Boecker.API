
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Domain.Events;

public class FollowUpConfirmedEvent : INotification
{
    public int ContractId { get; }
    public DateTime ScheduledDate { get; }
    public string ClientEmail { get; }

    public FollowUpConfirmedEvent(int contractId, DateTime scheduledDate, string clientEmail)
    {
        ContractId = contractId;
        ScheduledDate = scheduledDate;
        ClientEmail = clientEmail;
    }
}
