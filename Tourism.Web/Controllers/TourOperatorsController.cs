using Microsoft.AspNetCore.Mvc;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class TourOperatorsController : Controller
    {
        private readonly ITourOperatorService _tourOperatorService;

        public TourOperatorsController(ITourOperatorService tourOperatorService)
        {
            _tourOperatorService = tourOperatorService;
        }

        // GET: /TourOperators — Всички туроператори
        public async Task<IActionResult> Index()
        {
            var operators = await _tourOperatorService.GetAllAsync();

            var viewModels = operators.Select(o => new TourOperatorViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                Email = o.Email,
                PhoneNumber = o.PhoneNumber,
                LogoUrl = o.LogoUrl,
                TourCount = o.Tours.Count
            }).ToList();

            return View(viewModels);
        }

        // GET: /TourOperators/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var op = await _tourOperatorService.GetByIdAsync(id);
            if (op == null) return NotFound();

            var viewModel = new TourOperatorViewModel
            {
                Id = op.Id,
                Name = op.Name,
                Description = op.Description,
                Email = op.Email,
                PhoneNumber = op.PhoneNumber,
                LogoUrl = op.LogoUrl,
                TourCount = op.Tours.Count
            };

            ViewBag.Tours = op.Tours.Select(t => new TourViewModel
            {
                Id = t.Id,
                Title = t.Title,
                PricePerPerson = t.PricePerPerson,
                DurationDays = t.DurationDays,
                ImageUrl = t.ImageUrl,
                DestinationName = t.Destination.Name
            }).ToList();

            return View(viewModel);
        }
    }
}