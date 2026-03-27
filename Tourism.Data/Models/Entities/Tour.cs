namespace Tourism.Data.Models.Entities
{
    public class Tour
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal PricePerPerson { get; set; }
        public decimal DiscountPercent { get; set; } = 0;

        public int DurationDays { get; set; }

        public int MaxParticipants { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public int DestinationId { get; set; }
        public int TourOperatorId { get; set; }
        public int CategoryId { get; set; }

        // Navigation
        public Destination Destination { get; set; } = null!;
        public TourOperator TourOperator { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<TourImage> Images { get; set; } = new List<TourImage>();
    }
}