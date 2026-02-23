using System.ComponentModel.DataAnnotations;

namespace Tourism.Web.Models.ViewModels
{
    public class TourViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 100000)]
        public decimal PricePerPerson { get; set; }

        [Required]
        [Range(1, 365)]
        public int DurationDays { get; set; }

        [Required]
        [Range(1, 1000)]
        public int MaxParticipants { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int DestinationId { get; set; }
        public string DestinationName { get; set; } = string.Empty;

        [Required]
        public int TourOperatorId { get; set; }
        public string TourOperatorName { get; set; } = string.Empty;

        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
