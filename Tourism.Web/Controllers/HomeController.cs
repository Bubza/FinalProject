using Microsoft.AspNetCore.Mvc;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourService _tourService;

        public HomeController(ITourService tourService)
        {
            _tourService = tourService;
        }

        // GET: / — Начална страница
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();

            var featuredTours = tours
                .OrderByDescending(t => t.CreatedAt)
                .Take(6)
                .Select(t => new TourViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    PricePerPerson = t.PricePerPerson,
                    DurationDays = t.DurationDays,
                    ImageUrl = t.ImageUrl,
                    StartDate = t.StartDate,
                    DestinationName = t.Destination.Name,
                    TourOperatorName = t.TourOperator.Name
                })
                .ToList();

            return View(featuredTours);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}
