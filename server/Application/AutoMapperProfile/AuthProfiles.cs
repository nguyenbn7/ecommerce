using AutoMapper;
using Ecommerce.Auth.Entities;
using Ecommerce.Auth.Models;

namespace Ecommerce.Application.AutoMapperProfile;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<AppUser, UserProfile>();
    }
}