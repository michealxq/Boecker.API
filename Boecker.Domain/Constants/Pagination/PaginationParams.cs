namespace Boecker.Domain.Constants.Pagination;

public class PaginationParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    public string? OrderBy { get; set; } // e.g., "IssueDate", "InvoiceNumber"
    public bool Descending { get; set; } = false;
}
