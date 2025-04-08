
namespace Boecker.Application.Lookup.Dtos;

public class LookupDto
{
    public string Name { get; set; } = default!;
    public List<string> Items { get; set; } = new();
}
