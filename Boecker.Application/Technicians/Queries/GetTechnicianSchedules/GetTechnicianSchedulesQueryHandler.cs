
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.Technicians.Queries.GetTechnicianSchedules;

public class GetTechnicianSchedulesQueryHandler(IServiceScheduleRepository repo)
    : IRequestHandler<GetTechnicianSchedulesQuery, List<ServiceSchedule>>
{
    public async Task<List<ServiceSchedule>> Handle(GetTechnicianSchedulesQuery request, CancellationToken cancellationToken)
    {
        return await repo.Query()
            .Include(s => s.Service)
            .Include(s => s.Contract)
            .Where(s => s.TechnicianId == request.TechnicianId)
            .OrderBy(s => s.DateScheduled)
            .ToListAsync(cancellationToken);
    }
}
