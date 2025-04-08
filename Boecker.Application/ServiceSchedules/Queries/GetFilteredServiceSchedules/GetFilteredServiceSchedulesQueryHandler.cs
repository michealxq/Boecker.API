
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.ServiceSchedules.Queries.GetFilteredServiceSchedules;

public class GetFilteredServiceSchedulesQueryHandler(IServiceScheduleRepository repo)
    : IRequestHandler<GetFilteredServiceSchedulesQuery, List<ServiceSchedule>>
{
    public async Task<List<ServiceSchedule>> Handle(GetFilteredServiceSchedulesQuery request, CancellationToken cancellationToken)
    {
        var query = repo.Query()
            .Include(s => s.Service)
            .Include(s => s.Technician)
            .Include(s => s.Contract)
            .AsQueryable();

        if (request.TechnicianId.HasValue)
            query = query.Where(s => s.TechnicianId == request.TechnicianId);

        if (request.DateScheduled.HasValue)
            query = query.Where(s => s.DateScheduled.Date == request.DateScheduled.Value.ToDateTime(TimeOnly.MinValue).Date);

        if (request.Status.HasValue)
            query = query.Where(s => s.Status == request.Status.Value);

        return await query.ToListAsync(cancellationToken);
    }
}
