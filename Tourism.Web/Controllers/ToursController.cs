using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class ToursController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IFavoriteTourService _favoriteService;

        public ToursController(ITourService tourService, IFavoriteTourService favoriteService)
        {
            _tourService = tourService;
            _favoriteService = favoriteService;
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

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                ViewBag.IsFavorite = await _favoriteService.IsFavoriteAsync(userId, id);
            }
            var reviews = tour.Reviews.Select(r => new ReviewViewModel
            {
                Id = r.Id,
                TourId = r.TourId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserName = r.UserId.Substring(0, 8) + "..."
            }).ToList();

            ViewBag.Reviews = reviews;
            return View(viewModel);
        }
    }
}