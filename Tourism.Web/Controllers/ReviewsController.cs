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
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;

        public ReviewsController(IReviewService reviewService, ITourService tourService, IBookingService bookingService)
        {
            _reviewService = reviewService;
            _tourService = tourService;
            _bookingService = bookingService;
        }

        // GET: /Reviews/Create?tourId=5
        public async Task<IActionResult> Create(int tourId)
        {
            if (User.IsInRole("Operator") || User.IsInRole("Admin"))
            {
                TempData["Error"] = "Operators and admins cannot write reviews.";
                return RedirectToAction("Details", "Tours", new { id = tourId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var userBookings = await _bookingService.GetByUserIdAsync(userId);
            var hasBooking = userBookings.Any(b => b.TourId == tourId &&
                (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Completed));

            if (!hasBooking)
            {
                TempData["Error"] = "You can only review tours you have booked.";
                return RedirectToAction("Details", "Tours", new { id = tourId });
            }

            var tour = await _tourService.GetByIdAsync(tourId);
            if (tour == null) return NotFound();

            return View(new ReviewViewModel { TourId = tourId, TourTitle = tour.Title });
        }

        // POST: /Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            if (User.IsInRole("Operator") || User.IsInRole("Admin"))
                return Forbid();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var userBookings = await _bookingService.GetByUserIdAsync(userId);
            var hasBooking = userBookings.Any(b => b.TourId == model.TourId &&
                (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Completed));

            if (!hasBooking)
            {
                TempData["Error"] = "You can only review tours you have booked.";
                return RedirectToAction("Details", "Tours", new { id = model.TourId });
            }

            if (!ModelState.IsValid) return View(model);

            var review = new Review
            {
                UserId = userId,
                TourId = model.TourId,
                Rating = model.Rating,
                Comment = model.Comment
            };

            await _reviewService.CreateAsync(review);
            TempData["Success"] = "Review published successfully!";
            return RedirectToAction("Details", "Tours", new { id = model.TourId });
        }

        // POST: /Reviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null || review.UserId != userId) return NotFound();
            var tourId = review.TourId;
            await _reviewService.DeleteAsync(id);
            return RedirectToAction("Details", "Tours", new { id = tourId });
        }
    }
}