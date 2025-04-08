namespace Boecker.Application.Invoices.Dtos;

public class ClientDetailsDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
