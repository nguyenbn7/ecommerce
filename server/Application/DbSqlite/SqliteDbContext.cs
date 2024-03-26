using Ecommerce.Shared.BaseDb;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.DbSqlite;

public class SqliteDbContext(DbContextOptions<SqliteDbContext> options, IHostEnvironment environment) : AppDbContext(options, environment)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<decimal>().HaveConversion(typeof(DecimalToDouble));
    }
}