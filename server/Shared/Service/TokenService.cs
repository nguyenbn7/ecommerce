using System.Security.Claims;

namespace Ecommerce.Shared.Service;

public interface TokenService
{
    string CreateJWTToken(IEnumerable<Claim> claims);
}