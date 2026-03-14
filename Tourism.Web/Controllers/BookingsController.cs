using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITourService _tourService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(IBookingService bookingService, ITourService tourService, UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _tourService = tourService;
            _userManager = userManager;
        }

        // GET: /Bookings
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var bookings = await _bookingService.GetByUserIdAsync(userId);

            var viewModels = bookings.Select(b => new BookingViewModel
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
            }).ToList();

            return View(viewModels);
        }

        // GET: /Bookings/Create?tourId=5
        public async Task<IActionResult> Create(int tourId)
        {
            if (User.IsInRole("Operator") || User.IsInRole("Admin"))
            {
                TempData["Error"] = "Operators and admins cannot book tours.";
                return RedirectToAction("Details", "Tours", new { id = tourId });
            }

            var tour = await _tourService.GetByIdAsync(tourId);
            if (tour == null) return NotFound();

            var bookedSpots = await _bookingService.GetBookedSpotsAsync(tourId);
            var availableSpots = tour.MaxParticipants - bookedSpots;

            if (availableSpots <= 0)
            {
                TempData["Error"] = "Sorry, this tour is fully booked.";
                return RedirectToAction("Details", "Tours", new { id = tourId });
            }

            var effectivePrice = tour.DiscountPercent > 0
                ? Math.Round(tour.PricePerPerson * (1 - tour.DiscountPercent / 100), 2)
                : tour.PricePerPerson;

            var viewModel = new BookingViewModel
            {
                TourId = tour.Id,
                TourTitle = tour.Title,
                DestinationName = tour.Destination.Name,
                TourStartDate = tour.StartDate,
                PricePerPerson = effectivePrice,
                NumberOfPeople = 1
            };

            ViewBag.AvailableSpots = availableSpots;
            return View(viewModel);
        }

        // POST: /Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (User.IsInRole("Operator") || User.IsInRole("Admin"))
                return Forbid();

            if (!ModelState.IsValid) return View(model);

            var tour = await _tourService.GetByIdAsync(model.TourId);
            if (tour == null) return NotFound();

            var bookedSpots = await _bookingService.GetBookedSpotsAsync(model.TourId);
            var availableSpots = tour.MaxParticipants - bookedSpots;

            if (model.NumberOfPeople > availableSpots)
            {
                ModelState.AddModelError("NumberOfPeople",
                    $"Not enough spots available. Available: {availableSpots}.");
                ViewBag.AvailableSpots = availableSpots;
                return View(model);
            }

            var effectivePrice = tour.DiscountPercent > 0
                ? Math.Round(tour.PricePerPerson * (1 - tour.DiscountPercent / 100), 2)
                : tour.PricePerPerson;

            var booking = new Booking
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                TourId = model.TourId,
                NumberOfPeople = model.NumberOfPeople,
                TotalPrice = effectivePrice * model.NumberOfPeople,
                Status = BookingStatus.Pending
            };

            await _bookingService.CreateAsync(booking);

            TempData["Success"] = "Booking placed successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Bookings/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var booking = await _bookingService.GetByIdAsync(id);

            if (booking == null || booking.UserId != userId) return NotFound();

            booking.Status = BookingStatus.Cancelled;
            await _bookingService.UpdateAsync(booking);

            TempData["Success"] = "Booking cancelled.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Bookings/Profile
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(userId);
            var bookings = await _bookingService.GetByUserIdAsync(userId);

            var viewModels = bookings.Select(b => new BookingViewModel
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
            }).ToList();

            var fullName = user?.FullName;
            ViewBag.DisplayName = !string.IsNullOrWhiteSpace(fullName) ? fullName : User.FindFirstValue(ClaimTypes.Email);
            ViewBag.UserEmail = User.FindFirstValue(ClaimTypes.Email);
            ViewBag.TotalTrips = viewModels.Count(b => b.Status == BookingStatus.Completed || b.Status == BookingStatus.Confirmed);
            ViewBag.TotalSpent = viewModels.Where(b => b.Status != BookingStatus.Cancelled).Sum(b => b.TotalPrice);

            return View(viewModels);
        }
    }
}