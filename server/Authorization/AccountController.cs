using System.Security.Claims;
using Ecommerce.Authorization.Entities;
using Ecommerce.Authorization.Models;
using Ecommerce.Authorization.Services;
using Ecommerce.Shared.Controllers;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Authorization;

public class AccountController(ILogger<AccountController> logger,
                               UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager,
                               ITokenGenerator tokenGenerator) : APIController(logger)
{
    private readonly UserManager<AppUser> userManager = userManager;
    private readonly SignInManager<AppUser> signInManager = signInManager;
    private readonly ITokenGenerator tokenGenerator = tokenGenerator;

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
            AccessToken = tokenGenerator.GenerateJWTToken(BuildUserClaims(user)),
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