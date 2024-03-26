namespace Ecommerce.Application.ServiceCollectionExtension;

public static class ApplicationCors
{
    public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {

        services.AddCors(opt =>
        {
            opt.AddPolicy("Allow all", policy =>
            {
                policy.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });

            if (environment.IsProduction())
            {
                opt.AddDefaultPolicy(policy =>
                {
                    var origins = configuration.GetValue<string[]>("ORIGINS") ?? throw new Exception("ORIGINS can not be null");

                    policy.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            }
        });

        return services;
    }
}