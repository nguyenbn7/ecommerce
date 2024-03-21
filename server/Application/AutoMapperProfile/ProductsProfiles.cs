using AutoMapper;
using Ecommerce.Application.AutoMapperResolver;
using Ecommerce.Products.Entities;
using Ecommerce.Products.Models;

namespace Ecommerce.Application.AutoMapperProfile;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductImageUrl>());
    }
}
