using System.Security.Claims;
using Ecommerce.Auth.Entities;
using Ecommerce.Auth.Models;
using Ecommerce.Auth.Services;
using Ecommerce.Shared.Controllers;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Auth;

public class AccountController(ILogger<AccountController> logger,
                               UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager,
                               ITokenGenerator tokenGenerator) : APIController(logger)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var unauthorizedResponse = new ErrorResponse("User name or Password is incorrect");

        var user = await _userManager.FindByNameAsync(loginDTO.Username);
        if (user == null)
            return Unauthorized(unauthorizedResponse);

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

        if (!result.Succeeded)
            return Unauthorized(unauthorizedResponse);

        return Ok(new LoginSuccess
        {
            AccessToken = _tokenGenerator.GenerateJWTToken(BuildUserClaims(user)),
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