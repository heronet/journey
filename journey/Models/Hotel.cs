namespace journey.Models;

public class Hotel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LocationOnMap { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AddedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
