using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Web.Data;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class ToursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tours — Каталог маршрути
        public async Task<IActionResult> Index(string? search, int? destinationId, decimal? maxPrice)
        {
            var query = _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .Include(t => t.Reviews)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Title.Contains(search) || t.Destination.Name.Contains(search));

            if (destinationId.HasValue)
                query = query.Where(t => t.DestinationId == destinationId);

            if (maxPrice.HasValue)
                query = query.Where(t => t.PricePerPerson <= maxPrice);

            var tours = await query
                .Select(t => new TourViewModel
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
                })
                .ToListAsync();

            return View(tours);
        }

        // GET: /Tours/Details/5 — Страница на маршрута
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .Include(t => t.Reviews)
                .FirstOrDefaultAsync(t => t.Id == id);

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

