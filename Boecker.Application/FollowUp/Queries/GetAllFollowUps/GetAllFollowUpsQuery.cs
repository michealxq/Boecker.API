
using Boecker.Application.FollowUp.Dtos;
using MediatR;

namespace Boecker.Application.FollowUp.Queries.GetAllFollowUps;

public class GetAllFollowUpsQuery : IRequest<List<FollowUpDto>>
{
    // You can later add filtering parameters if needed.
}
