using System.Security.Claims;
using Ecommerce.Module.Account.Entity;
using Ecommerce.Module.Account.Model;
using Ecommerce.Shared;
using Ecommerce.Shared.Base;
using Ecommerce.Shared.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Module.Account;

public class AccountController(ILogger<AccountController> logger,
                               UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager,
                               TokenService tokenService) : APIController(logger)
{
    private readonly UserManager<AppUser> userManager = userManager;
    private readonly SignInManager<AppUser> signInManager = signInManager;
    private readonly TokenService tokenService = tokenService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var unauthorizedResponse = new ErrorResponse("User name or Password is incorrect");

        var user = await userManager.FindByNameAsync(loginDTO.Username);
        if (user == null)
            return Unauthorized(unauthorizedResponse);

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

        if (!result.Succeeded)
            return Unauthorized(unauthorizedResponse);

        return Ok(new LoginSuccess
        {
            AccessToken = tokenService.CreateJWTToken(BuildUserClaims(user)),
        });
    }

    [HttpPost("Register")]
    public IActionResult Register()
    {
        return Ok();
    }

    private static List<Claim> BuildUserClaims(AppUser user)
    {
        return [
            new(ClaimTypes.NameIdentifier, user.UserName!)
        ];
    }
}