using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Application.ServiceCollectionExtension;

public static class ValidationErrors
{
    public static IServiceCollection ConfigureApiBehaviourOptions(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value?.Errors ?? [])
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                var errorResponse = new ValidationErrorResponse(errors);

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}