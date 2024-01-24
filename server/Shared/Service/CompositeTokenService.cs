using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Shared.Service;

public class CompositeTokenService(IConfiguration configuration) : TokenService
{
    private readonly IConfiguration configuration = configuration;

    public string CreateJWTToken(IEnumerable<Claim> claims)
    {
        var secret = configuration.GetValue<string>("JWT:Key") ?? throw new Exception("JWT:Key not provided");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = configuration.GetValue<string>("JWT:Issuer")
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}