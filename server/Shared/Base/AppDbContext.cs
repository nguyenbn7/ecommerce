using Ecommerce.Module.Account.Entity;
using Ecommerce.Module.Product.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Shared.Base;

// https://dev.to/moesmp/ef-core-multiple-database-providers-3gb7 
public abstract class AppDbContext(IConfiguration configuration) : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    protected readonly IConfiguration configuration = configuration;

    public required DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }));
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityUserToken<string>>();

        builder.Entity<AppUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<AppRole>()
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.Entity<AppUserRole>().ToTable("UserRoles");
        builder.Entity<AppRole>().ToTable("Roles");
        builder.Entity<AppUser>().ToTable("Users");
    }
}
