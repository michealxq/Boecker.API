
namespace Boecker.Domain.Entities;

public class LookupItem
{
    public int Id { get; set; }
    public int LookupId { get; set; }
    public string Value { get; set; } = default!;

    public Lookup Lookup { get; set; } = default!;
}
