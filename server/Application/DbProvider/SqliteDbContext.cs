using Ecommerce.Products.Entities;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.DbProvider;

public class SqliteDbContext(IConfiguration configuration) : AppDbContext(configuration)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite(configuration.GetConnectionString("SqliteConn"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>(p =>
        {
            p.Property(p => p.Price).HasColumnType("decimal(18, 2)").HasConversion<double>();
        });
    }
}