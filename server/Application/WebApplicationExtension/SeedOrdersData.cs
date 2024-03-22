using System.Text.Json;
using Ecommerce.Application.ServiceProviderExtension;
using Ecommerce.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.WebApplicationExtension;

public static class SeedOrdersData
{
    public static async Task SeedDeliveryMethodsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var dbContext = services.GetAppDbContext(app.Configuration);
        try
        {
            if (await dbContext.ShippingMethods.AnyAsync()) return;

            var deliveryMethodsData = await File.ReadAllTextAsync("FakeData/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<ShippingMethod>>(deliveryMethodsData);

            if (deliveryMethods == null)
            {
                logger.LogError("Can not get seed data: Delivery Method");
                return;
            }

            dbContext.ShippingMethods.AddRange(deliveryMethods);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occured during seed delivery method data: {}", ex.Message);
            logger.LogError("Details: {}", ex.StackTrace);
        }
    }
}