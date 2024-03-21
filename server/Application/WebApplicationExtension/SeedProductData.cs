using System.Text.Json;
using Ecommerce.Application.ServiceProviderExtension;
using Ecommerce.Products.Entities;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.WebApplicationExtension;

public static class SeedProductData
{
    public static async Task SeedProductsDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var context = services.GetAppDbContext(app.Configuration);

        await CreateProductBrandsAsync(context, logger);
        await CreateProductTypesAsync(context, logger);
        await CreateProductsAsync(context, logger);
    }

    private static async Task CreateProductBrandsAsync(AppDbContext context,
                                                    ILogger logger)
    {
        if (await context.ProductBrands.AnyAsync()) return;

        var brandsData = await File.ReadAllTextAsync("FakeData/brands.json");
        var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
        if (productBrands == null)
        {
            logger.LogError("Can not get seed data: Product Brand");
            return;
        }

        context.ProductBrands.AddRange(productBrands);
        await context.SaveChangesAsync();
    }

    private static async Task CreateProductTypesAsync(AppDbContext context,
                                                   ILogger logger)
    {
        if (await context.ProductTypes.AnyAsync()) return;

        var typesData = await File.ReadAllTextAsync("FakeData/types.json");
        var productTypes = JsonSerializer.Deserialize<List<ProductType>>(typesData);
        if (productTypes == null)
        {
            logger.LogError("Can not get seed data: Product Type");
            return;
        }

        context.ProductTypes.AddRange(productTypes);
        await context.SaveChangesAsync();
    }

    private static async Task CreateProductsAsync(AppDbContext context,
                                               ILogger logger)
    {
        if (await context.Products.AnyAsync()) return;

        var productsData = await File.ReadAllTextAsync("FakeData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
        if (products == null)
        {
            logger.LogError("Can not get seed data: Product");
            return;
        }

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}