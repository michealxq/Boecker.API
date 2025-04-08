
using AutoMapper;
using Boecker.Application.ServiceSchedules.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Queries.GetServiceScheduleById;

public class GetServiceScheduleByIdQueryHandler(
        IServiceScheduleRepository repo,
        IMapper mapper
    ) : IRequestHandler<GetServiceScheduleByIdQuery, ServiceScheduleDto>
{
    public async Task<ServiceScheduleDto> Handle(GetServiceScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (schedule == null) throw new Exception("Service Schedule not found");

        return mapper.Map<ServiceScheduleDto>(schedule);
    }
}
