namespace Tourism.Data.Models.Entities
{
    public class TourOperator
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Linked user account
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; } 


        // Navigation
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}