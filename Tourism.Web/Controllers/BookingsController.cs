using Microsoft.AspNetCore.Authorization;
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

        public BookingsController(IBookingService bookingService, ITourService tourService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
        }

        // GET: /Bookings — Моите резервации
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
            var tour = await _tourService.GetByIdAsync(tourId);
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

            var tour = await _tourService.GetByIdAsync(model.TourId);
            if (tour == null) return NotFound();

            var booking = new Booking
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                TourId = model.TourId,
                NumberOfPeople = model.NumberOfPeople,
                TotalPrice = tour.PricePerPerson * model.NumberOfPeople,
                Status = BookingStatus.Pending
            };

            await _bookingService.CreateAsync(booking);

            TempData["Success"] = "Резервацията е направена успешно!";
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

            TempData["Success"] = "Резервацията е отказана.";
            return RedirectToAction(nameof(Index));
        }
        // GET: /Bookings/Profile — Профил с история на пътуванията
        public async Task<IActionResult> Profile()
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

            ViewBag.UserEmail = User.FindFirstValue(ClaimTypes.Email);
            ViewBag.TotalTrips = viewModels.Count(b => b.Status == BookingStatus.Completed || b.Status == BookingStatus.Confirmed);
            ViewBag.TotalSpent = viewModels.Where(b => b.Status != BookingStatus.Cancelled).Sum(b => b.TotalPrice);

            return View(viewModels);
        }
    }
}
