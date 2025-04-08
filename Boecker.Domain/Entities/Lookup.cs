
namespace Boecker.Domain.Entities;

public class Lookup
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<LookupItem> Items { get; set; } = new List<LookupItem>();
}
