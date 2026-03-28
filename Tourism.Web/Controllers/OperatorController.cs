using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize(Roles = "Operator")]
    public class OperatorController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;
        private readonly IDestinationService _destinationService;
        private readonly ITourOperatorService _operatorService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<OperatorController> _logger;

        public OperatorController(
            ITourService tourService,
            IBookingService bookingService,
            IDestinationService destinationService,
            ITourOperatorService operatorService,
            UserManager<ApplicationUser> userManager,
            ILogger<OperatorController> logger)
        {
            _tourService = tourService;
            _bookingService = bookingService;
            _destinationService = destinationService;
            _operatorService = operatorService;
            _userManager = userManager;
            _logger = logger;
        }

        private async Task<TourOperator?> GetMyOperatorAsync()
        {
            var userId = _userManager.GetUserId(User);
            var all = await _operatorService.GetAllAsync();
            return all.FirstOrDefault(o => o.UserId == userId);
        }

        // GET: /Operator
        public async Task<IActionResult> Dashboard()
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            _logger.LogInformation("Operator dashboard accessed by {User} (Operator: {OperatorName})", User.Identity?.Name, op.Name);

            var tours = (await _tourService.GetAllAsync())
                .Where(t => t.TourOperatorId == op.Id).ToList();
            var allBookings = await _bookingService.GetAllAsync();
            var myBookings = allBookings.Where(b => tours.Any(t => t.Id == b.TourId)).ToList();

            var model = new OperatorDashboardViewModel
            {
                Operator = op,
                TotalTours = tours.Count,
                TotalBookings = myBookings.Count,
                PendingBookings = myBookings.Count(b => b.Status == BookingStatus.Pending),
                Revenue = myBookings
                    .Where(b => b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Completed)
                    .Sum(b => b.TotalPrice),
                PopularTours = tours
                    .OrderByDescending(t => t.Bookings.Count)
                    .Take(5)
                    .Select(t => new OperatorPopularTourItem
                    {
                        Title = t.Title,
                        BookingCount = t.Bookings.Count,
                        PricePerPerson = t.PricePerPerson
                    }).ToList()
            };

            return View(model);
        }

        public IActionResult NoOperator() => View();

        // ── TOURS ──
        public async Task<IActionResult> Tours()
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var tours = (await _tourService.GetAllAsync())
                .Where(t => t.TourOperatorId == op.Id).ToList();
            ViewBag.Operator = op;
            return View(tours);
        }

        public async Task<IActionResult> CreateTour()
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var destinations = await _destinationService.GetAllAsync();
            ViewBag.Destinations = new SelectList(destinations, "Id", "Name");
            ViewBag.Operator = op;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTour(Tour tour)
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            ModelState.Remove("Destination");
            ModelState.Remove("TourOperator");

            if (!ModelState.IsValid)
            {
                var destinations = await _destinationService.GetAllAsync();
                ViewBag.Destinations = new SelectList(destinations, "Id", "Name");
                ViewBag.Operator = op;
                return View(tour);
            }

            tour.TourOperatorId = op.Id;
            await _tourService.CreateAsync(tour);
            _logger.LogInformation("Operator {User} created tour '{Title}'", User.Identity?.Name, tour.Title);
            TempData["Success"] = "Tour added successfully!";
            return RedirectToAction(nameof(Tours));
        }

        public async Task<IActionResult> EditTour(int id)
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null || tour.TourOperatorId != op.Id) return NotFound();

            var destinations = await _destinationService.GetAllAsync();
            ViewBag.Destinations = new SelectList(destinations, "Id", "Name", tour.DestinationId);
            ViewBag.Operator = op;
            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTour(Tour tour)
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            ModelState.Remove("Destination");
            ModelState.Remove("TourOperator");

            if (!ModelState.IsValid)
            {
                var destinations = await _destinationService.GetAllAsync();
                ViewBag.Destinations = new SelectList(destinations, "Id", "Name");
                ViewBag.Operator = op;
                return View(tour);
            }

            tour.TourOperatorId = op.Id;
            await _tourService.UpdateAsync(tour);
            _logger.LogInformation("Operator {User} updated tour ID {TourId}", User.Identity?.Name, tour.Id);
            TempData["Success"] = "Tour updated!";
            return RedirectToAction(nameof(Tours));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var op = await GetMyOperatorAsync();
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null || tour.TourOperatorId != op?.Id) return NotFound();

            await _tourService.DeleteAsync(id);
            _logger.LogWarning("Operator {User} deleted tour ID {TourId}", User.Identity?.Name, id);
            TempData["Success"] = "Tour deleted.";
            return RedirectToAction(nameof(Tours));
        }

        // ── BOOKINGS ──
        public async Task<IActionResult> Bookings()
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var tours = (await _tourService.GetAllAsync())
                .Where(t => t.TourOperatorId == op.Id).ToList();
            var allBookings = await _bookingService.GetAllAsync();
            var myBookings = allBookings
                .Where(b => tours.Any(t => t.Id == b.TourId)).ToList();

            var userNames = new Dictionary<string, string>();
            foreach (var b in myBookings)
            {
                if (!userNames.ContainsKey(b.UserId))
                {
                    var user = await _userManager.FindByIdAsync(b.UserId);
                    userNames[b.UserId] = user?.FullName ?? user?.Email ?? b.UserId.Substring(0, 8) + "...";
                }
            }
            ViewBag.UserNames = userNames;
            ViewBag.Operator = op;
            return View(myBookings);
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
                _logger.LogInformation("Operator {User} confirmed booking ID {BookingId}", User.Identity?.Name, id);
            }
            TempData["Success"] = "Booking confirmed!";
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
                _logger.LogWarning("Operator {User} cancelled booking ID {BookingId}", User.Identity?.Name, id);
            }
            TempData["Success"] = "Booking cancelled.";
            return RedirectToAction(nameof(Bookings));
        }

        // ── PROFILE ──
        public async Task<IActionResult> Profile()
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");
            return View(op);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(TourOperator model)
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            ModelState.Remove("Tours");
            if (!ModelState.IsValid) return View(model);

            op.Name = model.Name;
            op.Description = model.Description;
            op.Email = model.Email;
            op.PhoneNumber = model.PhoneNumber;
            op.LogoUrl = model.LogoUrl;
            await _operatorService.UpdateAsync(op);
            _logger.LogInformation("Operator {User} updated their profile", User.Identity?.Name);
            TempData["Success"] = "Profile updated!";
            return RedirectToAction(nameof(Profile));
        }

        // ── DISCOUNTS ──
        public async Task<IActionResult> Discounts()
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var tours = (await _tourService.GetAllAsync())
                .Where(t => t.TourOperatorId == op.Id).ToList();

            return View(tours);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDiscount(int tourId, decimal discountPercent)
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var tour = await _tourService.GetByIdAsync(tourId);
            if (tour == null || tour.TourOperatorId != op.Id) return NotFound();

            tour.DiscountPercent = Math.Clamp(discountPercent, 0, 90);
            await _tourService.UpdateAsync(tour);
            _logger.LogInformation("Operator {User} set {Discount}% discount on tour '{Title}'", User.Identity?.Name, discountPercent, tour.Title);

            TempData["Success"] = discountPercent > 0
                ? $"Discount of {discountPercent:0}% set on \"{tour.Title}\"."
                : $"Discount removed from \"{tour.Title}\".";

            return RedirectToAction(nameof(Discounts));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDiscount(decimal bulkPercent)
        {
            var op = await GetMyOperatorAsync();
            if (op == null) return RedirectToAction("NoOperator");

            var tours = (await _tourService.GetAllAsync())
                .Where(t => t.TourOperatorId == op.Id).ToList();

            var percent = Math.Clamp(bulkPercent, 0, 90);
            foreach (var tour in tours)
            {
                tour.DiscountPercent = percent;
                await _tourService.UpdateAsync(tour);
            }

            _logger.LogInformation("Operator {User} applied bulk discount of {Discount}% to {Count} tours", User.Identity?.Name, percent, tours.Count);

            TempData["Success"] = percent > 0
                ? $"Bulk discount of {percent:0}% applied to all {tours.Count} tours."
                : $"All discounts removed from {tours.Count} tours.";

            return RedirectToAction(nameof(Discounts));
        }
    }
}