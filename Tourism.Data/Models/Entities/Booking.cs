using Tourism.Data.Models.Enums;

namespace Tourism.Data.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int TourId { get; set; }

        public int NumberOfPeople { get; set; }

        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Tour Tour { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!; 

    }
}