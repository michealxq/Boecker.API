
using Boecker.Domain.Entities;

namespace Boecker.Application.Common.Interfaces;

public interface IPdfService
{
    (string FilePath, byte[] FileBytes) GenerateInvoicePdf(Invoice invoice);
}