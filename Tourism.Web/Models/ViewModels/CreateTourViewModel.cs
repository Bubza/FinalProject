using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tourism.Web.Models.ViewModels
{
    public class CreateTourViewModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 100000)]
        public decimal PricePerPerson { get; set; }

        [Range(0, 90)]
        public decimal DiscountPercent { get; set; } = 0;

        [Required]
        [Range(1, 365)]
        public int DurationDays { get; set; }

        [Required]
        [Range(1, 1000)]
        public int MaxParticipants { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);

        [Required]
        public int DestinationId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Destinations { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
    