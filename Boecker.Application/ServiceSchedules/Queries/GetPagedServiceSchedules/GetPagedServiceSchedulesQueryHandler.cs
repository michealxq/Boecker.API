
using Boecker.Domain.Constants.Pagination;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.ServiceSchedules.Queries.GetPagedServiceSchedules;

public class GetPagedServiceSchedulesQueryHandler(IServiceScheduleRepository repo)
    : IRequestHandler<GetPagedServiceSchedulesQuery, PaginatedResult<ServiceSchedule>>
{
    public async Task<PaginatedResult<ServiceSchedule>> Handle(GetPagedServiceSchedulesQuery request, CancellationToken cancellationToken)
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

        if (!string.IsNullOrWhiteSpace(request.Pagination.OrderBy))
        {
            query = request.Pagination.OrderBy.ToLower() switch
            {
                "datescheduled" => request.Pagination.Descending
                    ? query.OrderByDescending(s => s.DateScheduled)
                    : query.OrderBy(s => s.DateScheduled),

                "status" => request.Pagination.Descending
                    ? query.OrderByDescending(s => s.Status)
                    : query.OrderBy(s => s.Status),

                _ => query.OrderByDescending(s => s.DateScheduled)
            };
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ServiceSchedule>(items, totalCount, request.Pagination.PageNumber, request.Pagination.PageSize);
    }
}