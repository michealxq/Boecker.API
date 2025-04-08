using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Boecker.Domain.Entities;
using Boecker.Application.Common.Interfaces;
using System.Globalization;

public class PdfService : IPdfService
{
    public (string FilePath, byte[] FileBytes) GenerateInvoicePdf(Invoice invoice)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var pdfBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(25);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                // Header
                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Rentokil Boecker").FontSize(18).Bold().FontColor(Colors.Red.Medium);
                        col.Item().Text("PEST CONTROL | GERM CONTROL | FOOD SAFETY | HYGIENE & SCENTING");
                        col.Item().Text("VAT #: 1779591-601");
                    });

                    row.ConstantItem(120).AlignRight().Element(e =>
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "boecker-logo.png");
                        if (File.Exists(imagePath))
                        {
                            var imageData = File.ReadAllBytes(imagePath);
                            e.Image(imageData).FitWidth();
                        }
                    });
                });

                page.Content().Column(col =>
                {
                    col.Item().PaddingVertical(10).Text("PROFORMA INVOICE").FontSize(14).Bold().AlignCenter();

                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item().Text($"M/S: {invoice.Client.Name}").Bold();
                            left.Item().Text($"Address: {invoice.Client.Address}");
                            left.Item().Text($"Phone: {invoice.Client.PhoneNumber}");
                        });

                        row.ConstantItem(180).Column(right =>
                        {
                            right.Item().Text($"Date: {invoice.IssueDate:dd/MM/yyyy}");
                            right.Item().Text($"Invoice No: {invoice.InvoiceNumber}");
                            right.Item().Text($"Period: {invoice.ValidFrom:dd/MM/yyyy} - {invoice.ValidTo:dd/MM/yyyy}");
                        });
                    });

                    col.Item().PaddingVertical(20).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                    col.Item().PaddingTop(20).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.RelativeColumn();
                            columns.ConstantColumn(40);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(60);
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(1).Text("REF").Bold();
                            header.Cell().BorderBottom(1).Text("COVERING").Bold();
                            header.Cell().BorderBottom(1).Text("QTY").Bold();
                            header.Cell().BorderBottom(1).Text("U.P.").Bold();
                            header.Cell().BorderBottom(1).Text("AMOUNT").Bold();
                        });

                        foreach (var item in invoice.InvoiceServices)
                        {
                            var service = item.Service;
                            var priceFormatted = item.Price.ToString("F2");

                            table.Cell().BorderBottom(0.5f).Text(service.ServiceCategory?.Name ?? "N/A");
                            table.Cell().BorderBottom(0.5f).Text(service.Description);
                            table.Cell().BorderBottom(0.5f).Text("1");
                            table.Cell().BorderBottom(0.5f).Text(priceFormatted);
                            table.Cell().BorderBottom(0.5f).Text(priceFormatted);
                        }
                    });

                    col.Item().PaddingTop(25).AlignRight().Column(summary =>
                    {
                        summary.Item().Row(row =>
                        {
                            row.RelativeItem().AlignRight().Text("TOTAL AMOUNT BEFORE TAX (HT):").Bold();
                            row.ConstantItem(70).Text(invoice.TotalBeforeTax.ToString("F2"));
                        });
                        summary.Item().Row(row =>
                        {
                            row.RelativeItem().AlignRight().Text($"VAT ({invoice.VATPercentage}%):").Bold();
                            row.ConstantItem(70).Text(invoice.VATAmount.ToString("F2"));
                        });
                        summary.Item().Row(row =>
                        {
                            row.RelativeItem().AlignRight().Text("NET VALUE ALL TAXES INCLUDED:").Bold();
                            row.ConstantItem(70).Text(invoice.TotalAfterTax.ToString("F2"));
                        });
                    });

                    col.Item().PaddingTop(30).Text($"TOTAL AMOUNT: Only {invoice.TotalAfterTax:F2} U.S. Dollars")
                        .Bold().FontSize(10);

                    col.Item().PaddingTop(40).Row(row =>
                    {
                        row.RelativeItem().Text("Prepared By: _______________");
                        row.RelativeItem().Text("Approved By: _______________");
                        row.RelativeItem().Text("Received By: _______________");
                    });
                });

                page.Footer().PaddingTop(30).AlignCenter().Text("Boecker Building | Wadih Naim Street | Furn Al Chebak | Hotline: 03 NO PEST")
                    .FontSize(9);
            });
        }).GeneratePdf();

        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "invoices");
        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, $"Invoice_{invoice.InvoiceNumber}.pdf");
        File.WriteAllBytes(filePath, pdfBytes);

        return (filePath, pdfBytes);
    }

}
