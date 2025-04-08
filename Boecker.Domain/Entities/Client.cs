namespace Boecker.Domain.Entities;

public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<Invoice> Invoices { get; set; } = new();
}
