namespace Boecker.Domain.Constants;

public enum InvoiceStatus
{
    Pending,
    PartiallyPaid, // 🆕 New status
    Paid,
    Canceled
}

