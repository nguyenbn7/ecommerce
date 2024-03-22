using System.Reflection;
using Ecommerce.Auth.Entities;
using Ecommerce.Orders.Entities;
using Ecommerce.Products.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Shared.BaseDb;

// https://dev.to/moesmp/ef-core-multiple-database-providers-3gb7 
public abstract class AppDbContext(DbContextOptions options, IHostEnvironment environment)
    : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{
    private readonly IHostEnvironment _environment = environment;

    public required DbSet<Product> Products { get; set; }
    public required DbSet<ProductBrand> ProductBrands { get; set; }
    public required DbSet<ProductType> ProductTypes { get; set; }
    public required DbSet<CustomerOrder> CustomerOrders { get; set; }
    public required DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }
    public required DbSet<ShippingMethod> ShippingMethods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (_environment.IsDevelopment())
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.FullName?.Contains("BaseDb") ?? false);
    }
}
