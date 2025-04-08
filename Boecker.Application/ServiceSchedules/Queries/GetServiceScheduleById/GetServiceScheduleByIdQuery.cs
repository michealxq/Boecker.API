
using Boecker.Application.ServiceSchedules.Dtos;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Queries.GetServiceScheduleById;

public class GetServiceScheduleByIdQuery : IRequest<ServiceScheduleDto>
{
    public int Id { get; set; }

    public GetServiceScheduleByIdQuery(int id)
    {
        Id = id;
    }
}
