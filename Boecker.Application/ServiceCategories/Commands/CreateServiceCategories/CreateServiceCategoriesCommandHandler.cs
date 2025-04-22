using AutoMapper;
using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.ServiceCategories.Commands.CreateServiceCategories;

internal class CreateServiceCategoriesCommandHandler(
    ILogger<CreateServiceCategoriesCommandHandler> logger,
    //IMapper mapper,
    IServiceCategoryRepository serviceCategoryRepository
    ) : IRequestHandler<CreateServiceCategoriesCommand, int>
{
    public async Task<int> Handle(CreateServiceCategoriesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new ServiceCategory {@ServiceCategory}", request);

        var services = request.Services.Select(s => new Service
        {
            Name = s.Name,
            Description = s.Description,
            Price = s.Price,
            EstimatedCompletionTime = s.EstimatedCompletionTime,
            RequiresFollowUp = s.RequiresFollowUp,
            FollowUpPeriod = s.RequiresFollowUp ? s.FollowUpPeriod : null
        }).ToList();

        var category = new ServiceCategory
        {
            Name = request.Name,
            Description = request.Description,
            Services = services
        };

        await serviceCategoryRepository.AddAsync(category);
        return category.ServiceCategoryId;
    }

}

