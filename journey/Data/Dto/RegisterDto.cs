using System.ComponentModel.DataAnnotations;

namespace journey.Data.Dto;

public class RegisterDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
