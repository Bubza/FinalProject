namespace Tourism.Data.Models.Entities
{
    public class FavoriteTour
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int TourId { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Tour Tour { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!; 

    }
}