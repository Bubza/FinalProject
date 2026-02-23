using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Web.Data;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class TourOperatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TourOperatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /TourOperators — Всички туроператори
        public async Task<IActionResult> Index()
        {
            var operators = await _context.TourOperators
                .Include(o => o.Tours)
                .Select(o => new TourOperatorViewModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                    LogoUrl = o.LogoUrl,
                    TourCount = o.Tours.Count
                })
                .ToListAsync();

            return View(operators);
        }

        // GET: /TourOperators/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var op = await _context.TourOperators
                .Include(o => o.Tours)
                    .ThenInclude(t => t.Destination)
                .FirstOrDefaultAsync(o => o.Id == id);

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