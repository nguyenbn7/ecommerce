using Ecommerce.Application.Middleware;
using Ecommerce.Auth.Services;
using Ecommerce.Baskets;
using Ecommerce.Orders;

namespace Ecommerce.Application.ServiceCollectionExtension;

public static class ApplicationServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddTransient<ApplicationExceptionHandler>();
        services.AddTransient<RouteNotFoundHandler>();

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}