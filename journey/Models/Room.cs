namespace journey.Models;

public class Room
{
    public Guid Id { get; set; }
    public string Catrgory { get; set; } = string.Empty;
    public double Price { get; set; }
    public bool AC { get; set; }
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
