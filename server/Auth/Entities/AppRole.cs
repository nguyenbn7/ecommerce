using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Auth.Entities;

public class AppRole : IdentityRole
{
    public DateTime CreateDate { get; set; } = DateTime.Now;

    // Relationship
    public List<AppUserRole> UserRoles { get; set; } = [];
}