
using AutoMapper;
using Boecker.Domain.Entities;

namespace Boecker.Application.FollowUp.Dtos;

public class FollowUpMappingProfile : Profile
{
    public FollowUpMappingProfile()
    {
        CreateMap<FollowUpSchedule, FollowUpDto>();
    }
}
