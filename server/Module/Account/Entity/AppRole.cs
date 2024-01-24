using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Module.Account.Entity;

public class AppRole : IdentityRole
{
    public DateTime CreateDate { get; set; } = DateTime.Now;

    // Relationship
    public List<AppUserRole> UserRoles { get; set; } = [];
}