using System.Reflection;
using Ecommerce.Shared.BaseDb;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.DbPostgre;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options, IHostEnvironment environment) : AppDbContext(options, environment)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.FullName?.Contains("DbPostgre") ?? false);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTime>().HaveConversion(typeof(DateTimeToDateTimeUtc));
    }
}