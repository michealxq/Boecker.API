using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boecker.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {

        var applicationAssembly = typeof(ApplicationExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);



        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        

        services.AddHttpContextAccessor();
    }
}
