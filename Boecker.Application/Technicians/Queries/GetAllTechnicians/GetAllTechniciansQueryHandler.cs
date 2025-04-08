
using AutoMapper;
using Boecker.Application.Technicians.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Technicians.Queries.GetAllTechnicians;

public class GetAllTechniciansQueryHandler(ITechnicianRepository repo, IMapper mapper)
    : IRequestHandler<GetAllTechniciansQuery, List<TechnicianDto>>
{
    public async Task<List<TechnicianDto>> Handle(GetAllTechniciansQuery request, CancellationToken cancellationToken)
    {
        var techs = await repo.GetAllAsync(cancellationToken);
        return mapper.Map<List<TechnicianDto>>(techs);
    }
}
