using System.Security.Claims;
using Ecommerce.Auth.Entities;
using Ecommerce.Auth.Models;
using Ecommerce.Auth.Services;
using Ecommerce.Shared.Controllers;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<LoginSuccess>> Login(LoginDTO loginDTO)
    {
        var unauthorizedResponse = new ErrorResponse("User name or Password is incorrect");

        var user = await _userManager.FindByNameAsync(loginDTO.Email);
        if (user == null)
            return Unauthorized(unauthorizedResponse);

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

        if (!result.Succeeded)
            return Unauthorized(unauthorizedResponse);

        return new LoginSuccess
        {
            DisplayName = user.DisplayName,
            AccessToken = _tokenGenerator.GenerateJWTToken(BuildUserClaims(user)),
        };
    }

    [HttpPost("Register")]
    public async Task<ActionResult<LoginSuccess>> Register(RegisterDTO registerDTO)
    {
        var user = await _userManager.FindByNameAsync(registerDTO.Email);
        if (user == null)
        {
            return BadRequest(new ErrorResponse(400, "Email address already in use"));
        }

        user = new AppUser
        {
            FullName = registerDTO.FullName,
            DisplayName = registerDTO.DisplayName,
            Email = registerDTO.Email,
            UserName = registerDTO.Email
        };

        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (!result.Succeeded)
            return BadRequest(new ErrorResponse(400));

        return new LoginSuccess
        {
            DisplayName = user.DisplayName,
            AccessToken = _tokenGenerator.GenerateJWTToken(BuildUserClaims(user)),
        };
    }

    [HttpGet("display-name")]
    [Authorize]
    public async Task<ActionResult<string>> GetDisplayName()
    {
        var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (email == null)
            return "";

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return "";

        return user.DisplayName;
    }

    private static List<Claim> BuildUserClaims(AppUser user)
    {
        return [
            new(ClaimTypes.NameIdentifier, user.UserName!)
        ];
    }
}