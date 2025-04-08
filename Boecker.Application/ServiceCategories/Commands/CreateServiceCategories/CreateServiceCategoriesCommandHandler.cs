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
    IServiceCategoryRepository serviceCategoryRepository,
    IServiceRepository serviceRepository) : IRequestHandler<CreateServiceCategoriesCommand, int>
{
    public async Task<int> Handle(CreateServiceCategoriesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new ServiceCategory {@ServiceCategory}", request);

        var category = new ServiceCategory
        {
            Name = request.Name,
            Description = request.Description,
            Services = await serviceRepository.GetByIdsAsync(request.ServiceIds)
        };

        await serviceCategoryRepository.AddAsync(category);
        return category.ServiceCategoryId;
    }
}

