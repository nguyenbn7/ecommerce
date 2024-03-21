using Ecommerce.Application.DbPostgre;
using Ecommerce.Application.DbSqlite;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.ServiceCollectionExtension;

public static class ApplicationDbContext
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var postgreConn = configuration.GetConnectionString("PostgreConn");

        if (!string.IsNullOrEmpty(postgreConn))
            return services.AddDbContext<AppDbContext, PostgreDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(postgreConn);
            });

        var sqliteConn = configuration.GetConnectionString("SqliteConn");

        if (!string.IsNullOrEmpty(sqliteConn))
            return services.AddDbContext<AppDbContext, SqliteDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlite(sqliteConn);
            });

        throw new Exception("Please provide database connection");
    }
}