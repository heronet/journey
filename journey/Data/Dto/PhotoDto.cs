namespace journey.Data.Dto;

public class PhotoDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
}
