using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Module.Account.Entity;

public class AppUserRole : IdentityUserRole<string>
{
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
    // Relationship
    public required AppUser User { get; set; }
    public required AppRole Role { get; set; }
}