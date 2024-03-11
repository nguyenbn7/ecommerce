using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Accounts.Entities;

public class AppUser : IdentityUser
{
    public DateTime CreateDate { get; set; } = DateTime.Now;

    // Relationship
    public HashSet<AppUserRole> UserRoles { get; set; } = [];
}