using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Accounts.Models;

public class LoginDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}