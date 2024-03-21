using Ecommerce.Application.ServiceProviderExtension;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.WebApplicationExtension;

public static class ApplicationMigrations
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var dbContext = services.GetAppDbContext(app.Configuration);

        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during migration: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}