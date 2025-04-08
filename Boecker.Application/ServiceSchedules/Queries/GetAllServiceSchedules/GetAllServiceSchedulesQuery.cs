
using Boecker.Application.ServiceSchedules.Dtos;
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Queries.GetAllServiceSchedules;

public class GetAllServiceSchedulesQuery : IRequest<List<ServiceScheduleDto>> { }
