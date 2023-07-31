namespace journey.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Stars { get; set; }
        public User User { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Hotel Hotel { get; set; } = null!;
        public Guid HotelId { get; set; }
    }
}