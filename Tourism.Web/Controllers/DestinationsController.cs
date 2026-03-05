using Microsoft.AspNetCore.Mvc;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly ITourService _tourService;

        public DestinationsController(IDestinationService destinationService, ITourService tourService)
        {
            _destinationService = destinationService;
            _tourService = tourService;
        }

        // GET: /Destinations — Всички дестинации с филтри
        public async Task<IActionResult> Index(string? search, string? country)
        {
            var destinations = await _destinationService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
                destinations = destinations.Where(d => d.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                    || d.Country.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(country))
                destinations = destinations.Where(d => d.Country == country);

            var viewModels = destinations.Select(d => new DestinationViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Country = d.Country,
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                TourCount = d.Tours.Count
            }).ToList();

            // За dropdown филтъра по държава
            var allDestinations = await _destinationService.GetAllAsync();
            ViewBag.Countries = allDestinations.Select(d => d.Country).Distinct().OrderBy(c => c).ToList();

            return View(viewModels);
        }

        // GET: /Destinations/Details/5 — Детайли за дестинация с маршрути
        public async Task<IActionResult> Details(int id)
        {
            var destination = await _destinationService.GetByIdAsync(id);
            if (destination == null) return NotFound();

            var viewModel = new DestinationViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Country = destination.Country,
                Description = destination.Description,
                ImageUrl = destination.ImageUrl,
                TourCount = destination.Tours.Count
            };

            ViewBag.Tours = destination.Tours.Select(t => new TourViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                PricePerPerson = t.PricePerPerson,
                DurationDays = t.DurationDays,
                ImageUrl = t.ImageUrl,
                StartDate = t.StartDate
            }).ToList();

            return View(viewModel);
        }
    }
}
