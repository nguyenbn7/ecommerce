using Ecommerce.Application.DbProvider;
using Ecommerce.Auth.Entities;
using Ecommerce.Seeding;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Extension;

public static class ForWebApplication
{
    public static async Task ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        AppDbContext context;

        context = app.Configuration.GetValue<string>("DatabaseProvider") switch
        {
            "Postgre" => services.GetRequiredService<PostgreDbContext>(),
            _ => services.GetRequiredService<SqliteDbContext>()
        };

        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during migration: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }

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
                UserName = "admin",
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

    public static async Task SeedFakeDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        AppDbContext context = app.Configuration.GetValue<string>("DatabaseProvider") switch
        {
            "Postgre" => services.GetRequiredService<PostgreDbContext>(),
            _ => services.GetRequiredService<SqliteDbContext>(),
        };

        await SeedData.CreateProductBrandsAsync(context, logger);
        await SeedData.CreateProductTypesAsync(context, logger);
        await SeedData.CreateProductsAsync(context, logger);
    }
}