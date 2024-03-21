using StackExchange.Redis;

namespace Ecommerce.Application.ServiceCollectionExtension;

public static class RedisConfiguration
{
    public static IServiceCollection AddRedis(this IServiceCollection services,
                                              IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration.GetConnectionString("RedisConn");

            if (string.IsNullOrEmpty(connectionString?.Trim()))
                throw new Exception("Can not find or empty redis connection string (RedisConn)");

            var options = ConfigurationOptions.Parse(connectionString);
            return ConnectionMultiplexer.Connect(options);
        });

        return services;
    }
}