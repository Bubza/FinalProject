namespace Tourism.Data.Models.Entities
{
    public class Destination
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        // Navigation
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}