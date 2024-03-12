using AutoMapper;
using Ecommerce.Baskets.Entities;
using Ecommerce.Baskets.Models;
using Ecommerce.Products.Entities;
using Ecommerce.Products.Models;

namespace Ecommerce.Application;

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

public class BasketMapper : Profile
{
    public BasketMapper()
    {
        CreateMap<CustomerBasket, Basket>();
        CreateMap<CustomerBasketItem, BasketItem>();
    }
}