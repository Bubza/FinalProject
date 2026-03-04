using Microsoft.AspNetCore.Mvc;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class ToursController : Controller
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        // GET: /Tours — Каталог маршрути
        public async Task<IActionResult> Index(string? search, int? destinationId, decimal? maxPrice)
        {
            var tours = await _tourService.SearchAsync(search, destinationId, maxPrice);

            var viewModels = tours.Select(t => new TourViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                PricePerPerson = t.PricePerPerson,
                DurationDays = t.DurationDays,
                ImageUrl = t.ImageUrl,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                DestinationName = t.Destination.Name,
                TourOperatorName = t.TourOperator.Name,
                AverageRating = t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = t.Reviews.Count
            }).ToList();

            return View(viewModels);
        }

        // GET: /Tours/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);

            if (tour == null) return NotFound();

            var viewModel = new TourViewModel
            {
                Id = tour.Id,
                Title = tour.Title,
                Description = tour.Description,
                PricePerPerson = tour.PricePerPerson,
                DurationDays = tour.DurationDays,
                MaxParticipants = tour.MaxParticipants,
                ImageUrl = tour.ImageUrl,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                DestinationId = tour.DestinationId,
                DestinationName = tour.Destination.Name,
                TourOperatorId = tour.TourOperatorId,
                TourOperatorName = tour.TourOperator.Name,
                AverageRating = tour.Reviews.Any() ? tour.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = tour.Reviews.Count
            };

            return View(viewModel);
        }
    }
}