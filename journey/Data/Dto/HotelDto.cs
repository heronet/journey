using journey.Models;

namespace journey.Data.Dto;

public class HotelDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LocationOnMap { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<RoomDto>? Rooms { get; set; }
    public List<RatingDto>? Ratings { get; set; }
    public List<IFormFile> UploadPhotos { get; set; } = new List<IFormFile>();
    public List<PhotoDto>? Photos { get; set; }
}
