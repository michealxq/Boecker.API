
using AutoMapper;
using Boecker.Application.ServiceSchedules.Dtos;
using Boecker.Application.Technicians.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.Technicians.Queries.GetTechnicianSchedules;

public class GetTechnicianSchedulesQueryHandler(IServiceScheduleRepository repo, IMapper mapper)
    : IRequestHandler<GetTechnicianSchedulesQuery, List<ServiceScheduleDto>>
{
    public async Task<List<ServiceScheduleDto>> Handle(GetTechnicianSchedulesQuery request, CancellationToken cancellationToken)
    {
        var tech = await repo.Query()
            .Include(s => s.Service)
            .Include(s => s.Contract)
                .ThenInclude(c => c.Customer)
            .Where(s => s.TechnicianId == request.TechnicianId)
            .OrderBy(s => s.DateScheduled)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ServiceScheduleDto>>(tech);
    }
}
