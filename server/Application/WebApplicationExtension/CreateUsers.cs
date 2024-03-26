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
        var configuration = services.GetRequiredService<IConfiguration>();
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

            var adminPassword = configuration.GetValue<string>("Password:Admin") ?? throw new Exception("Password:Admin is not set");

            admin = new AppUser
            {
                FullName = "Administrator",
                UserName = "admin",
                DisplayName = "Admin"
            };

            await userManager.CreateAsync(admin, adminPassword);

            await userManager.AddToRoleAsync(admin, "Administrator");

            logger.LogInformation("Create {} succesffully", admin.FullName);
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
        var configuration = services.GetRequiredService<IConfiguration>();
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
                var demoCustomerPassword = configuration.GetValue<string>("Password:DemoCustomer") ?? throw new Exception("Password:DemoCustomer is not set");

                customer = new AppUser
                {
                    FullName = "Demo Customer",
                    DisplayName = "Demo Customer",
                    Email = "demo.customer@test.com",
                    UserName = "demo.customer@test.com"
                };

                await userManager.CreateAsync(customer, demoCustomerPassword);

                await userManager.AddToRoleAsync(customer, "Customer");
                logger.LogInformation("Create {} succesffully", customer.FullName);
            }

            var customer1 = await userManager.FindByEmailAsync("demo.customer1@test.com");

            if (customer1 == null)
            {
                var demoCustomer1Password = configuration.GetValue<string>("Password:DemoCustomer1") ?? throw new Exception("Password:DemoCustomer1 is not set");

                customer1 = new AppUser
                {
                    FullName = "Demo Customer1",
                    DisplayName = "Demo Customer1",
                    Email = "demo.customer1@test.com",
                    UserName = "demo.customer1@test.com"
                };


                await userManager.CreateAsync(customer1, demoCustomer1Password);

                await userManager.AddToRoleAsync(customer1, "Customer");

                logger.LogInformation("Create {} succesffully", customer1.FullName);
            }
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create demo users: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}