using Boecker.Domain.Constants;
using MediatR;

namespace Boecker.Application.Invoices.Commands.CreateInvoices;

public record CreateInvoiceCommand(
    string InvoiceNumber,
    DateTime IssueDate,
    DateTime ValidFrom,
    DateTime ValidTo,
    decimal VATPercentage,
    InvoiceStatus Status,
    int ClientId,
    List<CreateInvoiceServiceDto> Services,
    bool IsRecurring,
    RecurrencePeriod? RecurrencePeriod
) : IRequest<int>;

public record CreateInvoiceServiceDto(int ServiceId, int DurationDays);

