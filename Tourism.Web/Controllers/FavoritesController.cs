using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tourism.Data.Models.Entities;
using Tourism.Services;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteTourService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoritesController(IFavoriteTourService favoriteService, UserManager<ApplicationUser> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _favoriteService.GetByUserIdAsync(userId!);

            var viewModels = favorites
                .Where(f => f.Tour != null)
                .Select(f => new Tourism.Web.Models.ViewModels.TourViewModel
                {
                    Id = f.Tour!.Id,
                    Title = f.Tour.Title,
                    Description = f.Tour.Description ?? "",
                    PricePerPerson = f.Tour.PricePerPerson,
                    DurationDays = f.Tour.DurationDays,
                    ImageUrl = f.Tour.ImageUrl,
                    StartDate = f.Tour.StartDate,
                    EndDate = f.Tour.EndDate,
                    DestinationName = f.Tour.Destination?.Name ?? "",
                    TourOperatorName = f.Tour.TourOperator?.Name ?? "",
                    AverageRating = f.Tour.Reviews != null && f.Tour.Reviews.Any()
                        ? f.Tour.Reviews.Average(r => r.Rating) : 0,
                    ReviewCount = f.Tour.Reviews?.Count ?? 0
                });

            return View(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            await _favoriteService.AddAsync(userId!, id);
            TempData["Success"] = "Tour added to saved.";
            return Redirect(returnUrl ?? Url.Action("Details", "Tours", new { id })!);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            await _favoriteService.RemoveAsync(userId!, id);
            TempData["Success"] = "Tour removed from saved.";
            return Redirect(returnUrl ?? Url.Action("Details", "Tours", new { id })!);
        }
    }
}