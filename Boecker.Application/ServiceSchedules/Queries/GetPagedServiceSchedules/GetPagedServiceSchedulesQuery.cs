
using Boecker.Domain.Constants.Pagination;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Queries.GetPagedServiceSchedules;

public record GetPagedServiceSchedulesQuery(
    PaginationParams Pagination,
    int? TechnicianId,
    DateOnly? DateScheduled,
    ScheduleStatus? Status
) : IRequest<PaginatedResult<ServiceSchedule>>;
