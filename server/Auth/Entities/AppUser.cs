using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Auth.Entities;

public class AppUser : IdentityUser
{
    public required string FullName { get; set; }
    public required string DisplayName { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    // Relationship
    public HashSet<AppUserRole> UserRoles { get; set; } = [];
}