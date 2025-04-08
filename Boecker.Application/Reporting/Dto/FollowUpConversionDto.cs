
namespace Boecker.Application.Reporting.Dto;

public class FollowUpConversionDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public int ConfirmedCount { get; set; }
    public int DeclinedCount { get; set; }
    public decimal ConversionRate { get; set; } // Not read-only anymore
}



