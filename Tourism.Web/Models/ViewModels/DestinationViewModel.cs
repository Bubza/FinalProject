using System.ComponentModel.DataAnnotations;

namespace Tourism.Web.Models.ViewModels
{
    public class DestinationViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int TourCount { get; set; }
    }
}