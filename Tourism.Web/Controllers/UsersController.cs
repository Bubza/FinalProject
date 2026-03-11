using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;
using Tourism.Services;
using Tourism.Web.Models.ViewModels;

namespace Tourism.Web.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class UsersController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IBookingService bookingService, UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var bookings = await _bookingService.GetByUserIdAsync(id);
            var bookingList = bookings.ToList();

            ViewBag.DisplayName = user.FullName;
            ViewBag.UserEmail = user.Email;
            ViewBag.PageTitle = user.FullName + "'s Profile";
            ViewBag.TotalTrips = bookingList.Count(b => b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Completed);
            ViewBag.TotalSpent = bookingList.Where(b => b.Status != BookingStatus.Cancelled).Sum(b => b.TotalPrice);

            var vms = bookingList.Select(b => new BookingViewModel
            {
                TourId = b.TourId,
                TourTitle = b.Tour?.Title ?? "",
                DestinationName = b.Tour?.Destination?.Name ?? "",
                TourStartDate = b.Tour?.StartDate ?? b.BookedAt,
                PricePerPerson = b.Tour?.PricePerPerson ?? 0,
                NumberOfPeople = b.NumberOfPeople,
                TotalPrice = b.TotalPrice,
                BookedAt = b.BookedAt,
                Status = b.Status
            });

            return View("~/Views/Bookings/Profile.cshtml", vms);
        }
    }
}