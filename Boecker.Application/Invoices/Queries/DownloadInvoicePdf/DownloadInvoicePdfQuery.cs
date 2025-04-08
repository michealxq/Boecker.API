
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.Application.Invoices.Queries.DownloadInvoicePdf;

public record DownloadInvoicePdfQuery(int InvoiceId) : IRequest<FileStreamResult>;
