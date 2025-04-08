
using MediatR;

namespace Boecker.Application.InvoiceServices.Commands.MarkInvoiceServiceAsCompleted;

public record MarkInvoiceServiceAsCompletedCommand(int InvoiceServiceId) : IRequest;
