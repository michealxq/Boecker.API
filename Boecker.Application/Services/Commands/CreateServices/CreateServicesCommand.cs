using MediatR;

namespace Boecker.Application.Services.Commands.CreateServices;

public record CreateServicesCommand(
    string Name,
    string Description,
    decimal Price,
    int EstimatedCompletionTime,
    bool RequiresFollowUp,
    int ServiceCategoryId
) : IRequest<int>;
