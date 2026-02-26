using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tourism.Web.Data;
using Tourism.Web.Models.Entities;
using Tourism.Web.Models.Enums;

namespace Tourism.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin — Dashboard
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalTours = await _context.Tours.CountAsync();
            ViewBag.TotalBookings = await _context.Bookings.CountAsync();
            ViewBag.TotalReviews = await _context.Reviews.CountAsync();
            ViewBag.TotalOperators = await _context.TourOperators.CountAsync();
            ViewBag.PendingBookings = await _context.Bookings.CountAsync(b => b.Status == BookingStatus.Pending);

            ViewBag.PopularTours = await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.Bookings)
                .OrderByDescending(t => t.Bookings.Count)
                .Take(5)
                .Select(t => new { t.Title, DestinationName = t.Destination.Name, BookingCount = t.Bookings.Count })
                .ToListAsync();

            return View();
        }

        // ===== TOURS =====
        public async Task<IActionResult> Tours()
        {
            var tours = await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .ToListAsync();
            return View(tours);
        }

        public async Task<IActionResult> CreateTour()
        {
            ViewBag.Destinations = new SelectList(await _context.Destinations.ToListAsync(), "Id", "Name");
            ViewBag.TourOperators = new SelectList(await _context.TourOperators.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTour(Tour tour)
        {
            ModelState.Remove("Destination");
            ModelState.Remove("TourOperator");

            if (!ModelState.IsValid)
            {
                ViewBag.Destinations = new SelectList(await _context.Destinations.ToListAsync(), "Id", "Name");
                ViewBag.TourOperators = new SelectList(await _context.TourOperators.ToListAsync(), "Id", "Name");
                return View(tour);
            }
            tour.CreatedAt = DateTime.UtcNow;
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Маршрутът е добавен успешно!";
            return RedirectToAction(nameof(Tours));
        }

        public async Task<IActionResult> EditTour(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) return NotFound();
            ViewBag.Destinations = new SelectList(await _context.Destinations.ToListAsync(), "Id", "Name", tour.DestinationId);
            ViewBag.TourOperators = new SelectList(await _context.TourOperators.ToListAsync(), "Id", "Name", tour.TourOperatorId);
            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTour(Tour tour)
        {
            ModelState.Remove("Destination");
            ModelState.Remove("TourOperator");

            if (!ModelState.IsValid)
            {
                ViewBag.Destinations = new SelectList(await _context.Destinations.ToListAsync(), "Id", "Name");
                ViewBag.TourOperators = new SelectList(await _context.TourOperators.ToListAsync(), "Id", "Name");
                return View(tour);
            }
            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Маршрутът е обновен!";
            return RedirectToAction(nameof(Tours));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null) { _context.Tours.Remove(tour); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Маршрутът е изтрит.";
            return RedirectToAction(nameof(Tours));
        }

        // ===== BOOKINGS =====
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Tour).ThenInclude(t => t.Destination)
                .OrderByDescending(b => b.BookedAt)
                .ToListAsync();
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null) { booking.Status = BookingStatus.Confirmed; await _context.SaveChangesAsync(); }
            TempData["Success"] = "Резервацията е потвърдена!";
            return RedirectToAction(nameof(Bookings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null) { booking.Status = BookingStatus.Cancelled; await _context.SaveChangesAsync(); }
            TempData["Success"] = "Резервацията е отказана.";
            return RedirectToAction(nameof(Bookings));
        }

        // ===== DESTINATIONS =====
        public async Task<IActionResult> Destinations()
        {
            var destinations = await _context.Destinations.Include(d => d.Tours).ToListAsync();
            return View(destinations);
        }

        public IActionResult CreateDestination() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDestination(Destination destination)
        {
            ModelState.Clear();

            destination.Tours = new List<Tour>();
            destination.Description ??= string.Empty;
            destination.ImageUrl ??= string.Empty;

            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Дестинацията е добавена!";
            return RedirectToAction(nameof(Destinations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null) { _context.Destinations.Remove(destination); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Дестинацията е изтрита.";
            return RedirectToAction(nameof(Destinations));
        }
    }
}

