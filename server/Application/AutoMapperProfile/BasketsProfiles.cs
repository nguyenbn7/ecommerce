using AutoMapper;
using Ecommerce.Baskets.Entities;
using Ecommerce.Baskets.Models;

namespace Ecommerce.Application.AutoMapperProfile;

public class BasketMapper : Profile
{
    public BasketMapper()
    {
        CreateMap<CustomerBasket, Basket>();
        CreateMap<CustomerBasketItem, BasketItem>();
    }
}