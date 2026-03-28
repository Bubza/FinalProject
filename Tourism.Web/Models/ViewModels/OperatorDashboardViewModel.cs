using Tourism.Data.Models.Entities;

namespace Tourism.Web.Models.ViewModels
{
    public class OperatorDashboardViewModel
    {
        public TourOperator Operator { get; set; } = null!;
        public int TotalTours { get; set; }
        public int TotalBookings { get; set; }
        public int PendingBookings { get; set; }
        public decimal Revenue { get; set; }
        public List<OperatorPopularTourItem> PopularTours { get; set; } = new();
    }

    public class OperatorPopularTourItem
    {
        public string Title { get; set; } = string.Empty;
        public int BookingCount { get; set; }
        public decimal PricePerPerson { get; set; }
    }
}