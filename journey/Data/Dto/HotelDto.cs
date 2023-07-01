using journey.Models;

namespace journey.Data.Dto;

public class HotelDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LocationOnMap { get; set; } = string.Empty;
    public List<RoomDto>? Rooms { get; set; }
    public Hotel ToHotel()
    {
        return new Hotel
        {
            Title = Title,
            Location = Location,
            LocationOnMap = LocationOnMap
        };
    }
}
