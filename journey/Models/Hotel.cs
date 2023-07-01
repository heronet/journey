namespace journey.Models;

public class Hotel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LocationOnMap { get; set; } = string.Empty;
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
