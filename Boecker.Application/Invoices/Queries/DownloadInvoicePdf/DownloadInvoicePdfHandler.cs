
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.Application.Invoices.Queries.DownloadInvoicePdf;

public class DownloadInvoicePdfHandler : IRequestHandler<DownloadInvoicePdfQuery, FileStreamResult>
{
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IPdfService _pdfService;

    public DownloadInvoicePdfHandler(IInvoiceRepository invoiceRepo, IPdfService pdfService)
    {
        _invoiceRepo = invoiceRepo;
        _pdfService = pdfService;
    }

    public async Task<FileStreamResult> Handle(DownloadInvoicePdfQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepo.GetByIdAsync(request.InvoiceId);
        if (invoice is null)
            throw new Exception("Invoice not found.");

        (string filePath, byte[] fileByte) = _pdfService.GenerateInvoicePdf(invoice);

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var fileName = Path.GetFileName(filePath);
        return new FileStreamResult(stream, "application/pdf")
        {
            FileDownloadName = fileName
        };
    }
}

