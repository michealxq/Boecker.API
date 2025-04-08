
using Boecker.Domain.Constants;

namespace Boecker.Domain.Entities;

public class Contract
{
    public int ContractId { get; set; }

    public int CustomerId { get; set; }
    public Client Customer { get; set; } = default!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ContractStatus Status { get; set; }
    public bool IncludesFollowUp { get; set; }

    public ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public ICollection<FollowUpSchedule> FollowUps { get; set; } = new List<FollowUpSchedule>();

}

