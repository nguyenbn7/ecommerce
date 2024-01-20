using Ecommerce.Module.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data;

public class DataSeed
{
    public static async Task CreateAppAdminAsync(UserManager<AppUser> userManager)
    {
        var admin = await userManager.FindByNameAsync("admin");

        if (admin != null) return;

        admin = new AppUser
        {
            UserName = "admin",
        };

        // TODO: Not hard core right here
        await userManager.CreateAsync(admin, "P@ssw0rd");

        await userManager.AddToRoleAsync(admin, "Admin");
    }

    public static async Task CreateRolesAsync(RoleManager<AppRole> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;

        var roles = new List<string>()
        {
            "Admin",
            "Staff",
            "User"
        };

        foreach (var roleName in roles)
        {
            await roleManager.CreateAsync(new AppRole
            {
                Name = roleName
            });
        }
    }
}