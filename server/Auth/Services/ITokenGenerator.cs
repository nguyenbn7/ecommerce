using System.Security.Claims;

namespace Ecommerce.Auth.Services;

public interface ITokenGenerator
{
    string GenerateJWTToken(IEnumerable<Claim> claims);
}