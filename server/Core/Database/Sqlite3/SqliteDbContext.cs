using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Database.Sqlite3;

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
    }
}