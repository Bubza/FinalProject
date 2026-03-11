using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;
using Tourism.Services;

namespace Tourism.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;
        private readonly IReviewService _reviewService;
        private readonly IDestinationService _destinationService;
        private readonly ITourOperatorService _tourOperatorService;

        public AdminController(
            ITourService tourService,
            IBookingService bookingService,
            IReviewService reviewService,
            IDestinationService destinationService,
            ITourOperatorService tourOperatorService)
        {
            _tourService = tourService;
            _bookingService = bookingService;
            _reviewService = reviewService;
            _destinationService = destinationService;
            _tourOperatorService = tourOperatorService;
        }

        // GET: /Admin — Dashboard
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            var bookings = await _bookingService.GetAllAsync();
            var reviews = await _reviewService.GetAllAsync();
            var operators = await _tourOperatorService.GetAllAsync();

            ViewBag.TotalTours = tours.Count();
            ViewBag.TotalBookings = bookings.Count();
            ViewBag.TotalReviews = reviews.Count();
            ViewBag.TotalOperators = operators.Count();
            ViewBag.PendingBookings = bookings.Count(b => b.Status == BookingStatus.Pending);

            ViewBag.PopularTours = tours
                .OrderByDescending(t => t.Bookings.Count)
                .Take(5)
                .Select(t => new { t.Title, DestinationName = t.Destination.Name, BookingCount = t.Bookings.Count })
                .ToList();

            return View();
        }

        // ===== TOURS =====
        public async Task<IActionResult> Tours()
        {
            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }

        public async Task<IActionResult> CreateTour()
        {
            var destinations = await _destinationService.GetAllAsync();
            var operators = await _tourOperatorService.GetAllAsync();

            ViewBag.Destinations = new SelectList(destinations, "Id", "Name");
            ViewBag.TourOperators = new SelectList(operators, "Id", "Name");
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
                var destinations = await _destinationService.GetAllAsync();
                var operators = await _tourOperatorService.GetAllAsync();

                ViewBag.Destinations = new SelectList(destinations, "Id", "Name");
                ViewBag.TourOperators = new SelectList(operators, "Id", "Name");
                return View(tour);
            }

            await _tourService.CreateAsync(tour);
            TempData["Success"] = "Маршрутът е добавен успешно!";
            return RedirectToAction(nameof(Tours));
        }

        public async Task<IActionResult> EditTour(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            var destinations = await _destinationService.GetAllAsync();
            var operators = await _tourOperatorService.GetAllAsync();

            ViewBag.Destinations = new SelectList(destinations, "Id", "Name", tour.DestinationId);
            ViewBag.TourOperators = new SelectList(operators, "Id", "Name", tour.TourOperatorId);
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
                var destinations = await _destinationService.GetAllAsync();
                var operators = await _tourOperatorService.GetAllAsync();

                ViewBag.Destinations = new SelectList(destinations, "Id", "Name");
                ViewBag.TourOperators = new SelectList(operators, "Id", "Name");
                return View(tour);
            }

            await _tourService.UpdateAsync(tour);
            TempData["Success"] = "Маршрутът е обновен!";
            return RedirectToAction(nameof(Tours));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTour(int id)
        {
            await _tourService.DeleteAsync(id);
            TempData["Success"] = "Маршрутът е изтрит.";
            return RedirectToAction(nameof(Tours));
        }

        // ===== BOOKINGS =====
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _bookingService.GetAllAsync();
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking != null)
            {
                booking.Status = BookingStatus.Confirmed;
                await _bookingService.UpdateAsync(booking);
            }
            TempData["Success"] = "Резервацията е потвърдена!";
            return RedirectToAction(nameof(Bookings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking != null)
            {
                booking.Status = BookingStatus.Cancelled;
                await _bookingService.UpdateAsync(booking);
            }
            TempData["Success"] = "Резервацията е отказана.";
            return RedirectToAction(nameof(Bookings));
        }

        // ===== DESTINATIONS =====
        public async Task<IActionResult> Destinations()
        {
            var destinations = await _destinationService.GetAllAsync();
            return View(destinations);
        }

        public IActionResult CreateDestination() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDestination(Destination destination)
        {
            ModelState.Clear();

            await _destinationService.CreateAsync(destination);
            TempData["Success"] = "Дестинацията е добавена!";
            return RedirectToAction(nameof(Destinations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            await _destinationService.DeleteAsync(id);
            TempData["Success"] = "Дестинацията е изтрита.";
            return RedirectToAction(nameof(Destinations));
        }
    
    // ===== TOUR OPERATORS =====
        public async Task<IActionResult> Operators()
        {
            var operators = await _tourOperatorService.GetAllAsync();
            return View(operators);
        }

        public IActionResult CreateOperator() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOperator(TourOperator tourOperator)
        {
            ModelState.Remove("Tours");
            if (!ModelState.IsValid) return View(tourOperator);

            await _tourOperatorService.CreateAsync(tourOperator);
            TempData["Success"] = "Tour operator added successfully!";
            return RedirectToAction(nameof(Operators));
        }

        public async Task<IActionResult> EditOperator(int id)
        {
            var op = await _tourOperatorService.GetByIdAsync(id);
            if (op == null) return NotFound();
            return View(op);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOperator(TourOperator tourOperator)
        {
            ModelState.Remove("Tours");
            if (!ModelState.IsValid) return View(tourOperator);

            await _tourOperatorService.UpdateAsync(tourOperator);
            TempData["Success"] = "Tour operator updated!";
            return RedirectToAction(nameof(Operators));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOperator(int id)
        {
            await _tourOperatorService.DeleteAsync(id);
            TempData["Success"] = "Tour operator deleted.";
            return RedirectToAction(nameof(Operators));
        }
    }
}