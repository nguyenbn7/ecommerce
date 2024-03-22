using AutoMapper;
using Ecommerce.Orders.Entities;
using Ecommerce.Orders.Models;

namespace Ecommerce.Application.AutoMapperProfile;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<AddressDTO, OrderAddress>();
    }
}