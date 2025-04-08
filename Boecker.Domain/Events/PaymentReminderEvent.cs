
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Domain.Events;

public record PaymentReminderEvent(Invoice Invoice) : INotification;
