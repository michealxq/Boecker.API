namespace Boecker.Domain.Entities;

public class ServiceCategory
{
    public int ServiceCategoryId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Service> Services { get; set; } = new();
}
