
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Module.Account;

public class LoginDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}