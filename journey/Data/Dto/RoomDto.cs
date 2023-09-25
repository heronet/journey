using journey.Models;

namespace journey.Data.Dto;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public double Price { get; set; }
    public bool AC { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid HotelId { get; set; }
    public List<IFormFile> UploadPhotos { get; set; } = new List<IFormFile>();
    public List<PhotoDto>? Photos { get; set; }
}
