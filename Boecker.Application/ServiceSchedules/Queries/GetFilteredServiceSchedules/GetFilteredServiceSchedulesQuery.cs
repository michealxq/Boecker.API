
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Queries.GetFilteredServiceSchedules;

public record GetFilteredServiceSchedulesQuery(
    int? TechnicianId,
    DateOnly? DateScheduled,
    ScheduleStatus? Status
) : IRequest<List<ServiceSchedule>>;
