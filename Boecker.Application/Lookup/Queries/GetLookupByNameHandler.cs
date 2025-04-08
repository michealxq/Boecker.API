
using Boecker.Application.Lookup.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Lookup.Queries;

public class GetLookupByNameHandler : IRequestHandler<GetLookupByNameQuery, LookupDto?>
{
    private readonly ILookupRepository _lookupRepository;

    public GetLookupByNameHandler(ILookupRepository lookupRepository)
    {
        _lookupRepository = lookupRepository;
    }

    public async Task<LookupDto?> Handle(GetLookupByNameQuery request, CancellationToken cancellationToken)
    {
        var lookup = await _lookupRepository.GetByNameAsync(request.Name, cancellationToken);

        return lookup == null ? null : new LookupDto
        {
            Name = lookup.Name,
            Items = lookup.Items.Select(i => i.Value).ToList()
        };
    }
}


