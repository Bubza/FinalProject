using Tourism.Data.Models.Entities;

namespace Tourism.Web.Models.ViewModels
{
    public class ToursIndexViewModel
    {
        public List<TourViewModel> Tours { get; set; } = new();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
