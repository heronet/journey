namespace journey.Models;

public class Photo
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
}
