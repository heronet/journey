using journey.Models;

namespace journey.Data.Dto;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Catrgory { get; set; } = string.Empty;
    public double Price { get; set; }
    public Guid HotelId { get; set; }
}
