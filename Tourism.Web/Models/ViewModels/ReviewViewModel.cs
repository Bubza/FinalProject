using System.ComponentModel.DataAnnotations;

namespace Tourism.Web.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TourId { get; set; }
        public string TourTitle { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Това поле е задължително!")]
        [StringLength(1000, MinimumLength = 10)]
        public string Comment { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
