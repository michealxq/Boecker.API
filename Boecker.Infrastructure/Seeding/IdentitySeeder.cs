
using Boecker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Boecker.Infrastructure.Seeding;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Seeder"); 

        logger.LogInformation("Seeding started...");

        string[] roles = ["Admin", "User"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
                logger.LogInformation($"Role created: {role}");
            }
        }

        // Seed default Admin user
        string adminEmail = "admin@boecker.com";
        string adminPassword = "Admin123!"; 

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                
            }
            else
            {
                logger.LogInformation("Admin user already exists.");
            }
        }
    } 
}
