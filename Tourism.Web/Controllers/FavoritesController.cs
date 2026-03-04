using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteTourService _favoriteService;

        public FavoritesController(IFavoriteTourService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // GET: /Favorites — Любими маршрути
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var favorites = await _favoriteService.GetByUserIdAsync(userId);

            var viewModels = favorites.Select(f => new TourViewModel
            {
                Id = f.Tour.Id,
                Title = f.Tour.Title,
                Description = f.Tour.Description,
                PricePerPerson = f.Tour.PricePerPerson,
                DurationDays = f.Tour.DurationDays,
                ImageUrl = f.Tour.ImageUrl,
                StartDate = f.Tour.StartDate,
                DestinationName = f.Tour.Destination.Name,
                TourOperatorName = f.Tour.TourOperator.Name
            }).ToList();

            return View(viewModels);
        }

        // POST: /Favorites/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _favoriteService.AddAsync(userId, id);

            TempData["Success"] = "Маршрутът е добавен в любими!";
            return RedirectToAction("Details", "Tours", new { id });
        }

        // POST: /Favorites/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _favoriteService.RemoveAsync(userId, id);

            TempData["Success"] = "Маршрутът е премахнат от любими.";
            return RedirectToAction(nameof(Index));
        }
    }
}