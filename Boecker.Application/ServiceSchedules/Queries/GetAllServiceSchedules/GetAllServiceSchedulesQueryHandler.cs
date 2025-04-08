
using AutoMapper;
using Boecker.Application.ServiceSchedules.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.ServiceSchedules.Queries.GetAllServiceSchedules;

public class GetAllServiceSchedulesQueryHandler(
    IServiceScheduleRepository repo,
    IMapper mapper
) : IRequestHandler<GetAllServiceSchedulesQuery, List<ServiceScheduleDto>>
{
    public async Task<List<ServiceScheduleDto>> Handle(GetAllServiceSchedulesQuery request, CancellationToken cancellationToken)
    {
        var query = repo.Query()
                        .Include(s => s.Service)
                        .Include(s => s.Technician)
                        .Include(s => s.Contract)
                            .ThenInclude(c => c.Customer);
                        

        var entities = await query.ToListAsync(cancellationToken);

        return mapper.Map<List<ServiceScheduleDto>>(entities);
    }
}
