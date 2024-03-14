using Ecommerce.Application.DbConverters;
using Ecommerce.Products.Entities;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.DbProvider;

public class PostgreDbContext(IConfiguration configuration, IWebHostEnvironment environment) : AppDbContext(configuration, environment)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreConn"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>(p =>
        {
            p.Property(p => p.Price).HasColumnType("decimal(18, 2)");
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTime>().HaveConversion(typeof(DateTimeToDateTimeUtc));
    }
}