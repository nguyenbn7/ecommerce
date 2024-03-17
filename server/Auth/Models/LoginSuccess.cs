namespace Ecommerce.Auth.Models;

public class LoginSuccess
{
    public required string DisplayName { get; set; }
    public required string AccessToken { get; set; }
}