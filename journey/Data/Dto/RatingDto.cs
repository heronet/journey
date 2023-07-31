namespace journey.Data.Dto
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Stars { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Guid HotelId { get; set; }
    }
}