using Boecker.Domain.Entities;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boecker.Infrastructure.Seeding;

internal class DatabaseSeeder(ApplicationDbContext dbContext, ILogger<DatabaseSeeder> logger) : IDatabaseSeeder
{
    public async Task Seed()
    {
        logger.LogInformation("🔹 Starting Database Seeding...");
        if (await dbContext.Database.CanConnectAsync())
        {
            logger.LogInformation("✅ Database Connection Successful.");
            if (!dbContext.ServiceCategories.Any())
            {
                var categories = GetServiceCategories();
                dbContext.ServiceCategories.AddRange(categories);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("✅ Service Categories Seeded.");
            }

            if (!dbContext.Services.Any())
            {
                var services = GetServices();
                dbContext.Services.AddRange(services);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Clients.Any())
            {
                var clients = GetClients();
                dbContext.Clients.AddRange(clients);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Invoices.Any())
            {
                var invoices = GetInvoices();
                dbContext.Invoices.AddRange(invoices);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.InvoiceServices.Any())
            {
                var invoiceServices = GetInvoiceServices();
                dbContext.InvoiceServices.AddRange(invoiceServices);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Lookups.Any())
            {
                var paymentMethods = new Lookup
                {
                    Name = "PaymentMethods",
                    Description = "Available methods for payment",
                    Items = new List<LookupItem>
            {
                new() { Value = "Cash" },
                new() { Value = "Credit" },
                new() { Value = "Bank Transfer" }
            }
                };

                var serviceStatuses = new Lookup
                {
                    Name = "ServiceStatuses",
                    Description = "Statuses for service scheduling",
                    Items = new List<LookupItem>
            {
                new() { Value = "Scheduled" },
                new() { Value = "Completed" },
                new() { Value = "Canceled" }
            }
                };

                var invoiceStatuses = new Lookup
                {
                    Name = "InvoiceStatuses",
                    Description = "Statuses of invoices",
                    Items = new List<LookupItem>
            {
                new() { Value = "Pending" },
                new() { Value = "Paid" },
                new() { Value = "Canceled" }
            }
                };

                var followUpStatuses = new Lookup
                {
                    Name = "FollowUpStatuses",
                    Description = "Statuses of optional follow-up services",
                    Items = new List<LookupItem>
            {
                new() { Value = "Pending" },
                new() { Value = "Confirmed" },
                new() { Value = "Declined" },
                new() { Value = "Completed" }
            }
                };

                dbContext.Lookups.AddRange(paymentMethods, serviceStatuses, invoiceStatuses, followUpStatuses);
                await dbContext.SaveChangesAsync();
            }

        }
    }

    private IEnumerable<ServiceCategory> GetServiceCategories()
    {
        List<ServiceCategory> serviceCategories = [

        new() { Name = "OS-SV-GGFT", Description = "Operational Specialist performing a Service Visit at GGFT site" },
        new() { Name = "PM-SV-SPM", Description = "Pest Management Service Visit supervised" }
        ];
        return serviceCategories;
    }

    private IEnumerable<Service> GetServices()
    {
        List<Service> services = [
        
        new() { Name = "The Extermination of Crawling Insects & Rodents", Description = "Schedule Of Services:-One Basic Service -One Replenishing Service -One Control Treatment , taking place 6 months after BS -One year warranty with free call-backs", Price = 0.00M, EstimatedCompletionTime = 1, RequiresFollowUp = true, ServiceCategoryId = 1 },
        new() { Name = "", Description = "", Price = 117.12M, EstimatedCompletionTime = 1, RequiresFollowUp = false, ServiceCategoryId = 2 }
        
        ];
        return services;
    }

    private IEnumerable<Client> GetClients()
    {
        List<Client> clients = [
        
        new()
        {
            
            Name = "Raymond Kamil Assaad Residence",
            Address = "Matn District-Antelias-Mount Lebanon",
            PhoneNumber = "+961 3622764-+961 3755301",
            Email = "raymond@gmail.com"
        }
        ];
        return clients;
    }

    private IEnumerable<Invoice> GetInvoices()
    {
        List<Invoice> invoices = [
        
        new()
        {
            
            InvoiceNumber = "OF 234958",
            IssueDate = new DateTime(2024, 07, 18),
            ValidFrom = new DateTime(2024, 07, 31),
            ValidTo = new DateTime(2025, 07, 30),
            TotalBeforeTax = 117.12M,
            VATPercentage = 11.0M,
            VATAmount = 12.88M,
            TotalAfterTax = 130.00M,
            ClientId = 1
        }
        ];
        return invoices;
    }

    private IEnumerable<InvoiceService> GetInvoiceServices()
    {
        List<InvoiceService> invoiceServices = [
        
            new() {  InvoiceId = 1, ServiceId = 1, Price = 0.00M, DurationDays = 365, Completed = false },
            new() {  InvoiceId = 1, ServiceId = 2, Price = 117.12M, DurationDays = 365, Completed = false }
        ];
        return invoiceServices;
    }
}
