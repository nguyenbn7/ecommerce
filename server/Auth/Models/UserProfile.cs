namespace Ecommerce.Auth.Models;

public class UserProfile
{
    public int UserId { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
}