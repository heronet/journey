namespace journey.Models;

public class Room
{
    public Guid Id { get; set; }
    public string Catrgory { get; set; } = string.Empty;
    public double Price { get; set; }
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
}
