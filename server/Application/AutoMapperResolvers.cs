using AutoMapper;
using Ecommerce.Products.Entities;
using Ecommerce.Products.Models;

namespace Ecommerce.Application;

public class ProductImageUrl : IValueResolver<Product, ProductDto, string?>
{
    private readonly IConfiguration _configuration;

    public ProductImageUrl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? Resolve(Product source,
                           ProductDto destination,
                           string? destMember,
                           ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _configuration["ApiUrl"] + source.PictureUrl;
        }
        return null;
    }
}