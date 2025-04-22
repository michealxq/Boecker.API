
using Boecker.Application.Payments.Dtos;
using MediatR;

namespace Boecker.Application.Payments.Queries.GetInvoicePaymentSummary;

public record GetInvoicePaymentSummaryQuery(int InvoiceId) : IRequest<PaymentSummaryDto>;
