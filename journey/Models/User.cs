using Microsoft.AspNetCore.Identity;

namespace journey.Models;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
