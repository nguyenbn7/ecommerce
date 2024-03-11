using Ecommerce.Accounts.Entities;
using Ecommerce.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Extension;

public static class WebApplicationAddon
{
    public static async Task ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        AppDbContext context;

        context = app.Configuration.GetValue<string>("DatabaseProvider") switch
        {
            "Sqlite" => services.GetRequiredService<SqliteDbContext>(),
            _ => services.GetRequiredService<PostgreDbContext>(),
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
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            var admin = await userManager.FindByNameAsync("admin");

            if (admin != null) return;

            admin = new AppUser
            {
                UserName = "admin",
            };

            // TODO: Not hard core right here
            await userManager.CreateAsync(admin, "P@ssw0rd");

            await userManager.AddToRoleAsync(admin, "Admin");
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create system admin: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }

    public static async Task CreateSystemRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            if (await roleManager.Roles.AnyAsync()) return;

            var roles = new List<string>()
            {
                "Admin",
                "Staff",
                "User"
            };

            foreach (var roleName in roles)
            {
                await roleManager.CreateAsync(new AppRole
                {
                    Name = roleName
                });
            }
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during create system roles: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
            throw;
        }
    }
}