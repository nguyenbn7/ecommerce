using Ecommerce.Application.DbPostgre;
using Ecommerce.Application.DbSqlite;
using Ecommerce.Shared.BaseDb;

namespace Ecommerce.Application.ServiceProviderExtension;

public static class ApplicationDbContext
{
    public static AppDbContext GetAppDbContext(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var postgreConn = configuration.GetConnectionString("PostgreConn");

        if (!string.IsNullOrEmpty(postgreConn))
            return serviceProvider.GetRequiredService<PostgreDbContext>();

        var sqliteConn = configuration.GetConnectionString("SqliteConn");

        if (!string.IsNullOrEmpty(sqliteConn))
            return serviceProvider.GetRequiredService<SqliteDbContext>();

        throw new Exception("Please provide database connection");
    }
}