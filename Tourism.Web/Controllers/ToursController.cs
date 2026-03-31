using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Data.Models.Entities;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    public class ToursController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IFavoriteTourService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingService _bookingService;
        private readonly ICategoryService _categoryService;

        public ToursController(ITourService tourService, IFavoriteTourService favoriteService,
            UserManager<ApplicationUser> userManager, IBookingService bookingService,
            ICategoryService categoryService)
        {
            _tourService = tourService;
            _favoriteService = favoriteService;
            _userManager = userManager;
            _bookingService = bookingService;
            _categoryService = categoryService;
        }

        // GET: /Tours
        public async Task<IActionResult> Index(string? search, decimal? minPrice, decimal? maxPrice,
            string? duration, int? minRating, string? sortBy, string? startDate, string? endDate,
            int? people, int? categoryId)
        {
            var tours = await _tourService.SearchAsync(search, null, maxPrice);
            var filtered = tours.AsEnumerable();

            if (minPrice.HasValue)
                filtered = filtered.Where(t => t.PricePerPerson >= minPrice);

            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out var parsedStart))
                filtered = filtered.Where(t => t.StartDate.Date >= parsedStart.Date);

            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out var parsedEnd))
                filtered = filtered.Where(t => t.StartDate.Date <= parsedEnd.Date);

            if (people.HasValue && people > 0)
                filtered = filtered.Where(t => t.MaxParticipants >= people);

            if (!string.IsNullOrEmpty(duration))
            {
                filtered = duration switch
                {
                    "1-3" => filtered.Where(t => t.DurationDays >= 1 && t.DurationDays <= 3),
                    "4-7" => filtered.Where(t => t.DurationDays >= 4 && t.DurationDays <= 7),
                    "8+" => filtered.Where(t => t.DurationDays >= 8),
                    _ => filtered
                };
            }

            if (minRating.HasValue)
                filtered = filtered.Where(t => t.Reviews.Any() && t.Reviews.Average(r => r.Rating) >= minRating);

            if (categoryId.HasValue)
                filtered = filtered.Where(t => t.CategoryId == categoryId.Value);

            var viewModels = filtered.Select(t => new TourViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                PricePerPerson = t.PricePerPerson,
                DiscountPercent = t.DiscountPercent,
                DurationDays = t.DurationDays,
                ImageUrl = t.ImageUrl,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                DestinationName = t.Destination.Name,
                TourOperatorName = t.TourOperator.Name,
                AverageRating = t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = t.Reviews.Count,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name
            });

            viewModels = sortBy switch
            {
                "price_asc" => viewModels.OrderBy(t => t.PricePerPerson),
                "price_desc" => viewModels.OrderByDescending(t => t.PricePerPerson),
                "rating" => viewModels.OrderByDescending(t => t.AverageRating),
                "duration" => viewModels.OrderBy(t => t.DurationDays),
                "newest" => viewModels.OrderByDescending(t => t.StartDate),
                _ => viewModels
            };

            var categories = (await _categoryService.GetAllAsync()).Where(c => c.Tours.Any());

            var vm = new ToursIndexViewModel
            {
                Tours = viewModels.ToList(),
                Categories = categories
            };

            return View(vm);
        }

        // GET: /Tours/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new TourViewModel
            {
                Id = tour.Id,
                Title = tour.Title,
                Description = tour.Description,
                PricePerPerson = tour.PricePerPerson,
                DiscountPercent = tour.DiscountPercent,
                DurationDays = tour.DurationDays,
                MaxParticipants = tour.MaxParticipants,
                ImageUrl = tour.ImageUrl,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                DestinationId = tour.DestinationId,
                DestinationName = tour.Destination.Name,
                DestinationCountry = tour.Destination.Country,
                TourOperatorId = tour.TourOperatorId,
                TourOperatorName = tour.TourOperator.Name,
                AverageRating = tour.Reviews.Any() ? tour.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = tour.Reviews.Count
            };

            if (User.Identity?.IsAuthenticated == true)
            {
                ViewBag.IsFavorite = await _favoriteService.IsFavoriteAsync(userId!, id);

                var userBookings = await _bookingService.GetByUserIdAsync(userId!);
                ViewBag.HasBooking = userBookings.Any(b => b.TourId == id &&
                    (b.Status == Tourism.Data.Models.Enums.BookingStatus.Confirmed ||
                     b.Status == Tourism.Data.Models.Enums.BookingStatus.Completed));
            }

            var reviews = new List<ReviewViewModel>();
            foreach (var r in tour.Reviews)
            {
                var user = await _userManager.FindByIdAsync(r.UserId);
                reviews.Add(new ReviewViewModel
                {
                    Id = r.Id,
                    TourId = r.TourId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    UserName = !string.IsNullOrEmpty(user?.FullName) ? user.FullName : user?.Email ?? "Анонимен"
                });
            }
            ViewBag.Reviews = reviews;

            var recommendations = await _tourService.GetRecommendationsAsync(id, userId, 3);
            ViewBag.Recommendations = recommendations.Select(t => new TourViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                PricePerPerson = t.PricePerPerson,
                DurationDays = t.DurationDays,
                ImageUrl = t.ImageUrl,
                DestinationName = t.Destination.Name,
                AverageRating = t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = t.Reviews.Count
            }).ToList();

            ViewBag.BookedSpots = tour.Bookings
                .Where(b => b.Status != Tourism.Data.Models.Enums.BookingStatus.Cancelled)
                .Sum(b => b.NumberOfPeople);

            ViewBag.RatingBreakdown = Enumerable.Range(1, 5).Reverse()
                .Select(star => new { Star = star, Count = tour.Reviews.Count(r => r.Rating == star) })
                .ToList();

            ViewBag.TourImages = tour.Images.OrderBy(i => i.SortOrder).ToList();

            return View(viewModel);
        }
    }
}
