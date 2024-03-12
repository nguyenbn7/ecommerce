using System.Security.Claims;

namespace Ecommerce.Authorization.Services;

public interface ITokenGenerator
{
    string GenerateJWTToken(IEnumerable<Claim> claims);
}