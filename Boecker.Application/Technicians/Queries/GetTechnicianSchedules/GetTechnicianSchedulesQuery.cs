
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Application.Technicians.Queries.GetTechnicianSchedules;

public record GetTechnicianSchedulesQuery(int TechnicianId) : IRequest<List<ServiceSchedule>>;
