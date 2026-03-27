namespace Tourism.Data.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Bootstrap Icon class e.g. "bi-compass", "bi-bank"
        public string IconClass { get; set; } = "bi-tag";

        // Navigation
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}