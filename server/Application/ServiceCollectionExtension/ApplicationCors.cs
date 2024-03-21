namespace Ecommerce.Application.ServiceCollectionExtension;

public static class ApplicationCors
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        // TODO: Update Cor
        services.AddCors(opt =>
        {
            opt.AddPolicy("DevCor", policy =>
            {
                policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}