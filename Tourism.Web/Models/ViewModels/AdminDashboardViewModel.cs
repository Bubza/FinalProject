namespace Tourism.Web.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalTours { get; set; }
        public int TotalBookings { get; set; }
        public int TotalReviews { get; set; }
        public int TotalOperators { get; set; }
        public int TotalUsers { get; set; }
        public int PendingBookings { get; set; }
        public int ConfirmedBookings { get; set; }
        public int CancelledBookings { get; set; }
        public int CompletedBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<PopularTourItem> PopularTours { get; set; } = new();
        public List<TopRatedTourItem> TopRatedTours { get; set; } = new();
        public List<UpcomingTourItem> UpcomingTours { get; set; } = new();
    }

    public class PopularTourItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public int BookingCount { get; set; }
        public decimal Revenue { get; set; }
    }

    public class TopRatedTourItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public double AvgRating { get; set; }
        public int ReviewCount { get; set; }
    }

    public class UpcomingTourItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int MaxParticipants { get; set; }
        public int BookedSpots { get; set; }
        public int AvailableSpots { get; set; }
    }
}