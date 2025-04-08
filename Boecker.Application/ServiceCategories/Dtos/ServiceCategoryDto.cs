namespace Boecker.Application.ServiceCategories.Dtos;

public class ServiceCategoryDto
{
    public int ServiceCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int> ServiceIds { get; set; } = new();
}
