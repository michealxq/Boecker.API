
using Boecker.Application.InvoiceServices.Dtos;
using MediatR;

namespace Boecker.Application.InvoiceServices.Queries.GetIncompleteServices;

public class GetIncompleteServicesQuery : IRequest<List<IncompleteServiceDto>>
{
    public int ClientId { get; set; }
}
