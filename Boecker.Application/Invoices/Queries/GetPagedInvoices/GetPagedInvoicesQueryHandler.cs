
using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Domain.Constants.Filters;
using Boecker.Domain.Constants.Pagination;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetPagedInvoices;

public class GetPagedInvoicesQueryHandler : IRequestHandler<GetPagedInvoicesQuery, PaginatedResult<InvoiceDto>>
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public GetPagedInvoicesQueryHandler(IInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<InvoiceDto>> Handle(GetPagedInvoicesQuery request, CancellationToken cancellationToken)
    {
        var filter = new InvoiceFilter
        {
            ClientId = request.ClientId,
            IsRecurring = request.IsRecurring,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Status = request.Status
        };

        var result = await _repository.GetPagedAsync(request, filter);
        var dtoItems = _mapper.Map<List<InvoiceDto>>(result.Items);

        return new PaginatedResult<InvoiceDto>(dtoItems, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
