using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Domain.Settings;
using Boecker.Infrastructure.persistence;
using Boecker.Infrastructure.Repositories;
using Boecker.Infrastructure.Seeding;
using Boecker.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boecker.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BoeckerDb");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

        services.Configure<ReminderSettings>(configuration.GetSection("ReminderSettings"));
        services.AddHostedService<ReminderBackgroundService>();

        services.AddScoped<ITokenService, TokenService>();


        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceServiceRepository, InvoiceServiceRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<ILookupRepository, LookupRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IServiceScheduleRepository, ServiceScheduleRepository>();
        services.AddScoped<IFollowUpScheduleRepository, FollowUpScheduleRepository>();
        services.AddScoped<ITechnicianRepository, TechnicianRepository>();
        services.AddScoped<IFollowUpRepository, FollowUpRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();



    }
}
