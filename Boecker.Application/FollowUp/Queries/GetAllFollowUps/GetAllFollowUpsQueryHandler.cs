
using AutoMapper;
using Boecker.Application.FollowUp.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.FollowUp.Queries.GetAllFollowUps;

public class GetAllFollowUpsQueryHandler : IRequestHandler<GetAllFollowUpsQuery, List<FollowUpDto>>
{
    private readonly IFollowUpRepository _followUpRepository;
    private readonly IMapper _mapper;

    public GetAllFollowUpsQueryHandler(IFollowUpRepository followUpRepository, IMapper mapper)
    {
        _followUpRepository = followUpRepository;
        _mapper = mapper;
    }

    public async Task<List<FollowUpDto>> Handle(GetAllFollowUpsQuery request, CancellationToken cancellationToken)
    {
        var followUps = await _followUpRepository.GetAllAsync(cancellationToken);
        // Map the domain entities to FollowUpDto
        return _mapper.Map<List<FollowUpDto>>(followUps);
    }
}
