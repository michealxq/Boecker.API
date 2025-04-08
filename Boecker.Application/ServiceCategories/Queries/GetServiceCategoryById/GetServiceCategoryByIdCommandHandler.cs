using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Application.Invoices.Queries.GetInvoiceById;
using Boecker.Application.ServiceCategories.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.ServiceCategories.Queries.GetServiceCategoryById;

public class GetServiceCategoryByIdCommandHandler(ILogger<GetServiceCategoryByIdCommandHandler> logger,
    IServiceCategoryRepository serviceCategoryRepository,
    IMapper mapper) : IRequestHandler<GetServiceCategoryByIdCommand, ServiceCategoryDto?>
{
    public async Task<ServiceCategoryDto?> Handle(GetServiceCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Service Category {ServiceCategoryId}", request.ServiceCategoryId);
        var category = await serviceCategoryRepository.GetByIdAsync(request.ServiceCategoryId)
                ?? throw new NotFoundException(nameof(ServiceCategory), request.ServiceCategoryId.ToString());

        return category is null ? null : mapper.Map<ServiceCategoryDto>(category);
    }
}
