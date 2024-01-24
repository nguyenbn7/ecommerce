using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Database.Postgre;

public class PostgreDbContext(IConfiguration configuration) : AppDbContext(configuration)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreConn"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}