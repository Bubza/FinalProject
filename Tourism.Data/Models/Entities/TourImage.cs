namespace Tourism.Data.Models.Entities
{
    public class TourImage
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;

        // Navigation
        public Tour Tour { get; set; } = null!;
    }
}