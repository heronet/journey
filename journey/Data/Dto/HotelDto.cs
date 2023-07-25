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
    public Hotel ToHotel()
    {
        return new Hotel
        {
            Title = Title,
            Location = Location,
            LocationOnMap = LocationOnMap,
            Description = Description,
            Email = Email,
            Phone = Phone
        };
    }
}
