using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Application.Invoices.Queries.GetInvoiceById;
using Boecker.Application.ServiceCategories.Commands.CreateServiceCategories;
using Boecker.Application.ServiceCategories.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.ServiceCategories.Queries.GetAllServiceCategories;

public class GetAllServiceCategoriesCommandHandler(ILogger<GetAllServiceCategoriesCommandHandler> logger,
    IMapper mapper,
    IServiceCategoryRepository serviceCategoryRepository) : IRequestHandler<GetAllServiceCategoriesCommand, IEnumerable<ServiceCategoryDto>>
{
    public async Task<IEnumerable<ServiceCategoryDto>> Handle(GetAllServiceCategoriesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Service Categories ");
        var categories = await serviceCategoryRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ServiceCategoryDto>>(categories);

    }
}
