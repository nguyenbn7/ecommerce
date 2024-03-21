using Ecommerce.Application.Middleware;
using Ecommerce.Auth.Services;
using Ecommerce.Baskets;

namespace Ecommerce.Application.ServiceCollectionExtension;

public static class ApplicationServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddTransient<ApplicationExceptionHandler>();
        services.AddTransient<RouteNotFoundHandler>();

        return services;
    }
}