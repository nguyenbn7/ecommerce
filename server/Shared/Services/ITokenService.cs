using System.Security.Claims;

namespace Ecommerce.Shared.Services;

public interface ITokenService
{
    string GenerateJWTToken(IEnumerable<Claim> claims);
}