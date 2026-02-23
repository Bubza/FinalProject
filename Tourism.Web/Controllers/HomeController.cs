using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Web.Data;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: / — Начална страница
        public async Task<IActionResult> Index()
        {
            // Show latest 6 tours on the home page
            var featuredTours = await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
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
                .ToListAsync();

            return View(featuredTours);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}
