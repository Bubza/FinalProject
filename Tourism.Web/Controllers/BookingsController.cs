using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tourism.Web.Data;
using Tourism.Web.Models.Entities;
using Tourism.Web.Models.Enums;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Bookings — Моите резервации
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var bookings = await _context.Bookings
                .Include(b => b.Tour)
                    .ThenInclude(t => t.Destination)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookedAt)
                .Select(b => new BookingViewModel
                {
                    Id = b.Id,
                    TourId = b.TourId,
                    TourTitle = b.Tour.Title,
                    DestinationName = b.Tour.Destination.Name,
                    TourStartDate = b.Tour.StartDate,
                    NumberOfPeople = b.NumberOfPeople,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status,
                    BookedAt = b.BookedAt
                })
                .ToListAsync();

            return View(bookings);
        }

        // GET: /Bookings/Create?tourId=5
        public async Task<IActionResult> Create(int tourId)
        {
            var tour = await _context.Tours
                .Include(t => t.Destination)
                .FirstOrDefaultAsync(t => t.Id == tourId);

            if (tour == null) return NotFound();

            var viewModel = new BookingViewModel
            {
                TourId = tour.Id,
                TourTitle = tour.Title,
                DestinationName = tour.Destination.Name,
                TourStartDate = tour.StartDate,
                PricePerPerson = tour.PricePerPerson,
                NumberOfPeople = 1
            };

            return View(viewModel);
        }

        // POST: /Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var tour = await _context.Tours.FindAsync(model.TourId);
            if (tour == null) return NotFound();

            var booking = new Booking
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                TourId = model.TourId,
                NumberOfPeople = model.NumberOfPeople,
                TotalPrice = tour.PricePerPerson * model.NumberOfPeople,
                Status = BookingStatus.Pending
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Резервацията е направена успешно!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Bookings/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (booking == null) return NotFound();

            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Резервацията е отказана.";
            return RedirectToAction(nameof(Index));
        }
    }
}

