using System.Text.Json;
using Ecommerce.Products.Entities;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Seeding;

public class SeedData
{
    public static async Task CreateProductBrandsAsync(AppDbContext context,
                                                    ILogger logger)
    {
        if (await context.ProductBrands.AnyAsync()) return;

        var brandsData = await File.ReadAllTextAsync("Seeding/FakeData/brands.json");
        var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
        if (productBrands == null)
        {
            logger.LogError("Can not get seed data: Product Brand");
            return;
        }

        context.ProductBrands.AddRange(productBrands);
        await context.SaveChangesAsync();
    }

    public static async Task CreateProductTypesAsync(AppDbContext context,
                                                   ILogger logger)
    {
        if (await context.ProductTypes.AnyAsync()) return;

        var typesData = await File.ReadAllTextAsync("Seeding/FakeData/types.json");
        var productTypes = JsonSerializer.Deserialize<List<ProductType>>(typesData);
        if (productTypes == null)
        {
            logger.LogError("Can not get seed data: Product Type");
            return;
        }

        context.ProductTypes.AddRange(productTypes);
        await context.SaveChangesAsync();
    }

    public static async Task CreateProductsAsync(AppDbContext context,
                                               ILogger logger)
    {
        if (await context.Products.AnyAsync()) return;

        var productsData = await File.ReadAllTextAsync("Seeding/FakeData/products.json");
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