using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Authorization.Models;

public class LoginDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}