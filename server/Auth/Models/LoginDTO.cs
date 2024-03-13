using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Auth.Models;

public class LoginDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}