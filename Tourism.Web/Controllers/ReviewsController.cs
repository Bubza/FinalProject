using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tourism.Web.Data;
using Tourism.Web.Models.Entities;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Reviews/Create?tourId=5
        public async Task<IActionResult> Create(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
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

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Ревюто е публикувано успешно!";
            return RedirectToAction("Details", "Tours", new { id = model.TourId });
        }

        // POST: /Reviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (review == null) return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Tours", new { id = review.TourId });
        }
    }
}
