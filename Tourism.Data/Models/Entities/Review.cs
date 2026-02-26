namespace Tourism.Data.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int TourId { get; set; }

        public int Rating { get; set; } // 1–5

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Tour Tour { get; set; } = null!;
    }
}
