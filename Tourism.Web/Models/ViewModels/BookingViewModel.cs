using System.ComponentModel.DataAnnotations;
using Tourism.Web.Models.Enums;

namespace Tourism.Web.Models.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TourId { get; set; }
        public string TourTitle { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public DateTime TourStartDate { get; set; }

        [Required]
        [Range(1, 50)]
        public int NumberOfPeople { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal PricePerPerson { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime BookedAt { get; set; }
    }
}