using System.ComponentModel.DataAnnotations;

namespace Tourism.Web.Models.ViewModels
{
    public class TourOperatorViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string LogoUrl { get; set; } = string.Empty;

        public int TourCount { get; set; }
    }
}
