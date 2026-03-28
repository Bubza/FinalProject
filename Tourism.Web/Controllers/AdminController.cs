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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;
        private readonly IReviewService _reviewService;
        private readonly IDestinationService _destinationService;
        private readonly ITourOperatorService _tourOperatorService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(
            ITourService tourService,
            IBookingService bookingService,
            IReviewService reviewService,
            IDestinationService destinationService,
            ITourOperatorService tourOperatorService,
            UserManager<ApplicationUser> userManager)
        {
            _tourService = tourService;
            _bookingService = bookingService;
            _reviewService = reviewService;
            _destinationService = destinationService;
            _tourOperatorService = tourOperatorService;
            _userManager = userManager;
        }

        // GET: /Admin — Dashboard
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            var bookings = await _bookingService.GetAllAsync();
            var reviews = await _reviewService.GetAllAsync();
            var operators = await _tourOperatorService.GetAllAsync();
            var users = _userManager.Users.ToList();

            var bookingList = bookings.ToList();
            var tourList = tours.ToList();

            // Build upcoming tours with spot data
            var upcomingTours = tourList
                .Where(t => t.StartDate > DateTime.UtcNow)
                .OrderBy(t => t.StartDate)
                .Take(5)
                .Select(t =>
                {
                    var booked = t.Bookings
                        .Where(b => b.Status != BookingStatus.Cancelled)
                        .Sum(b => b.NumberOfPeople);
                    return new UpcomingTourItem
                    {
                        Id = t.Id,
                        Title = t.Title,
                        StartDate = t.StartDate,
                        MaxParticipants = t.MaxParticipants,
                        BookedSpots = booked,
                        AvailableSpots = t.MaxParticipants - booked
                    };
                }).ToList();

            var model = new AdminDashboardViewModel
            {
                TotalTours = tourList.Count,
                TotalBookings = bookingList.Count,
                TotalReviews = reviews.Count(),
                TotalOperators = operators.Count(),
                TotalUsers = users.Count,
                PendingBookings = bookingList.Count(b => b.Status == BookingStatus.Pending),
                ConfirmedBookings = bookingList.Count(b => b.Status == BookingStatus.Confirmed),
                CancelledBookings = bookingList.Count(b => b.Status == BookingStatus.Cancelled),
                CompletedBookings = bookingList.Count(b => b.Status == BookingStatus.Completed),
                TotalRevenue = bookingList
                    .Where(b => b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Completed)
                    .Sum(b => b.TotalPrice),
                PopularTours = tourList
                    .OrderByDescending(t => t.Bookings.Count)
                    .Take(5)
                    .Select(t => new PopularTourItem
                    {
                        Id = t.Id,
                        Title = t.Title,
                        DestinationName = t.Destination?.Name ?? "-",
                        BookingCount = t.Bookings.Count,
                        Revenue = t.Bookings
                            .Where(b => b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Completed)
                            .Sum(b => b.TotalPrice)
                    }).ToList(),
                TopRatedTours = tourList
                    .Where(t => t.Reviews.Any())
                    .OrderByDescending(t => t.Reviews.Average(r => r.Rating))
                    .Take(5)
                    .Select(t => new TopRatedTourItem
                    {
                        Id = t.Id,
                        Title = t.Title,
                        DestinationName = t.Destination?.Name ?? "-",
                        AvgRating = Math.Round(t.Reviews.Average(r => r.Rating), 1),
                        ReviewCount = t.Reviews.Count
                    }).ToList(),
                UpcomingTours = upcomingTours
            };

            return View(model);
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
            TempData["Success"] = "Tour added successfully!";
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
            TempData["Success"] = "Tour updated!";
            return RedirectToAction(nameof(Tours));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTour(int id)
        {
            await _tourService.DeleteAsync(id);
            TempData["Success"] = "Tour deleted.";
            return RedirectToAction(nameof(Tours));
        }

        // ===== BOOKINGS =====
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _bookingService.GetAllAsync();
            var userIds = bookings.Select(b => b.UserId).Distinct().ToList();
            var userNames = new Dictionary<string, string>();
            foreach (var uid in userIds)
            {
                var user = await _userManager.FindByIdAsync(uid);
                userNames[uid] = user?.FullName ?? user?.Email ?? uid.Substring(0, 8) + "...";
            }
            ViewBag.UserNames = userNames;
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
            }
            TempData["Success"] = "Booking cancelled.";
            return RedirectToAction(nameof(Bookings));
        }

        // ===== OPERATORS =====
        public async Task<IActionResult> Operators()
        {
            var operators = await _tourOperatorService.GetAllAsync();
            var allUsers = _userManager.Users.ToList();
            ViewBag.AllUsers = allUsers;
            return View(operators);
        }

        public async Task<IActionResult> AssignOperator()
        {
            var operators = await _tourOperatorService.GetAllAsync();
            var allUsers = _userManager.Users.ToList();
            ViewBag.Operators = new SelectList(operators, "Id", "Name");
            ViewBag.Users = new SelectList(allUsers, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignOperator(int operatorId, string userId)
        {
            var op = await _tourOperatorService.GetByIdAsync(operatorId);
            if (op == null) return NotFound();

            if (!string.IsNullOrEmpty(op.UserId) && op.UserId != userId)
            {
                var prevUser = await _userManager.FindByIdAsync(op.UserId);
                if (prevUser != null)
                    await _userManager.RemoveFromRoleAsync(prevUser, "Operator");
            }

            op.UserId = userId;
            await _tourOperatorService.UpdateAsync(op);

            var newUser = await _userManager.FindByIdAsync(userId);
            if (newUser != null && !await _userManager.IsInRoleAsync(newUser, "Operator"))
                await _userManager.AddToRoleAsync(newUser, "Operator");

            TempData["Success"] = "Operator account assigned successfully!";
            return RedirectToAction(nameof(Operators));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOperator(int id)
        {
            var op = await _tourOperatorService.GetByIdAsync(id);
            if (op != null)
            {
                if (!string.IsNullOrEmpty(op.UserId))
                {
                    var user = await _userManager.FindByIdAsync(op.UserId);
                    if (user != null)
                        await _userManager.RemoveFromRoleAsync(user, "Operator");
                }
                await _tourOperatorService.DeleteAsync(id);
            }
            TempData["Success"] = "Operator deleted.";
            return RedirectToAction(nameof(Operators));
        }

        // ===== DISCOUNTS =====
        public async Task<IActionResult> Discounts()
        {
            var tours = (await _tourService.GetAllAsync()).ToList();
            return View(tours);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDiscount(int tourId, decimal discountPercent)
        {
            var tour = await _tourService.GetByIdAsync(tourId);
            if (tour == null) return NotFound();

            tour.DiscountPercent = Math.Clamp(discountPercent, 0, 90);
            await _tourService.UpdateAsync(tour);

            TempData["Success"] = discountPercent > 0
                ? $"Discount of {discountPercent:0}% set on \"{tour.Title}\"."
                : $"Discount removed from \"{tour.Title}\".";

            return RedirectToAction(nameof(Discounts));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDiscount(decimal bulkPercent)
        {
            var tours = (await _tourService.GetAllAsync()).ToList();
            var percent = Math.Clamp(bulkPercent, 0, 90);

            foreach (var tour in tours)
            {
                tour.DiscountPercent = percent;
                await _tourService.UpdateAsync(tour);
            }

            TempData["Success"] = percent > 0
                ? $"Bulk discount of {percent:0}% applied to all {tours.Count} tours."
                : $"All discounts removed from {tours.Count} tours.";

            return RedirectToAction(nameof(Discounts));
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
            TempData["Success"] = "Destination added!";
            return RedirectToAction(nameof(Destinations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            await _destinationService.DeleteAsync(id);
            TempData["Success"] = "Destination deleted.";
            return RedirectToAction(nameof(Destinations));
        }
    }
}