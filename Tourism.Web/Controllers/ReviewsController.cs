using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Data.Models.Entities;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ITourService _tourService;

        public ReviewsController(IReviewService reviewService, ITourService tourService)
        {
            _reviewService = reviewService;
            _tourService = tourService;
        }

        // GET: /Reviews/Create?tourId=5
        public async Task<IActionResult> Create(int tourId)
        {
            var tour = await _tourService.GetByIdAsync(tourId);
            if (tour == null) return NotFound();

            return View(new ReviewViewModel { TourId = tourId, TourTitle = tour.Title });
        }

        // POST: /Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var review = new Review
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                TourId = model.TourId,
                Rating = model.Rating,
                Comment = model.Comment
            };

            await _reviewService.CreateAsync(review);

            TempData["Success"] = "Ревюто е публикувано успешно!";
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