using Ecommerce.Auth.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.WebApplicationExtension;

public static class CreateUsers
{
    public static async Task CreateSystemAdminAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            if ((await roleManager.FindByNameAsync("Administrator")) == null)
            {
                await roleManager.CreateAsync(new AppRole
                {
                    Name = "Administrator"
                });
            }

            var admin = await userManager.FindByNameAsync("admin");

            if (admin != null) return;

            admin = new AppUser
            {
                FullName = "Administrator",
                UserName = "admin",
                DisplayName = "Admin"
            };

            // TODO: Not hard core right here
            await userManager.CreateAsync(admin, "P@ssw0rd");

            await userManager.AddToRoleAsync(admin, "Administrator");
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create system admin: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }

    public static async Task CreateDemoCustomersAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            if ((await roleManager.FindByNameAsync("Customer")) == null)
            {
                await roleManager.CreateAsync(new AppRole
                {
                    Name = "Customer"
                });
            }

            var customer = await userManager.FindByEmailAsync("demo.customer@test.com");

            if (customer == null)
            {
                customer = new AppUser
                {
                    FullName = "Demo Customer",
                    DisplayName = "Demo Customer",
                    Email = "demo.customer@test.com",
                    UserName = "demo.customer@test.com"
                };

                await userManager.CreateAsync(customer, "P@ssw0rd");

                await userManager.AddToRoleAsync(customer, "Customer");
            }

            var customer1 = await userManager.FindByEmailAsync("demo.customer1@test.com");

            if (customer1 == null)
            {
                customer1 = new AppUser
                {
                    FullName = "Demo Customer1",
                    DisplayName = "Demo Customer1",
                    Email = "demo.customer1@test.com",
                    UserName = "demo.customer1@test.com"
                };


                await userManager.CreateAsync(customer1, "P@ssw0rd");

                await userManager.AddToRoleAsync(customer1, "Customer");
            }
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create demo users: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}