using AutoMapper;
using Ecommerce.Products.Entities;
using Ecommerce.Products.Models;

namespace Ecommerce.Application;

public class ProductImageUrl(IConfiguration configuration) : IValueResolver<Product, ProductDto, string?>
{
    private readonly IConfiguration _configuration = configuration;

    public string? Resolve(Product source,
                           ProductDto destination,
                           string? destMember,
                           ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            var baseUriString = _configuration["IMAGE_BASE_URL"];
            if (baseUriString != null)
            {
                var uri = new Uri(new Uri(baseUriString), new Uri(source.PictureUrl, UriKind.Relative));
                return uri.AbsoluteUri;
            }
            return source.PictureUrl;
        }
        return null;
    }
}