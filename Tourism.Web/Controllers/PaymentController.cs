using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;
using Tourism.Services;

namespace Tourism.Web.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITourService _tourService;
        private readonly IPaymentService _paymentService;

        public PaymentController(IBookingService bookingService, ITourService tourService, IPaymentService paymentService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
            _paymentService = paymentService;
        }

        // GET: /Payment/Checkout
        public IActionResult Checkout()
        {
            var json = TempData["PendingBooking"] as string;
            if (string.IsNullOrEmpty(json))
                return RedirectToAction("Index", "Tours");

            TempData.Keep("PendingBooking");

            var data = JsonSerializer.Deserialize<PendingBookingData>(json)!;
            ViewBag.TourTitle = data.TourTitle;
            ViewBag.TotalPrice = data.TotalPrice;
            ViewBag.NumberOfPeople = data.NumberOfPeople;
            ViewBag.TourId = data.TourId;

            return View();
        }

        // POST: /Payment/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string cardNumber, string expiry, string cvv, string cardHolder)
        {
            var json = TempData["PendingBooking"] as string;
            if (string.IsNullOrEmpty(json))
                return RedirectToAction("Index", "Tours");

            var data = JsonSerializer.Deserialize<PendingBookingData>(json)!;

            //basic validation
            var cleanCard = cardNumber?.Replace(" ", "") ?? "";
            if (cleanCard.Length < 16 || string.IsNullOrWhiteSpace(expiry) ||
                string.IsNullOrWhiteSpace(cvv) || string.IsNullOrWhiteSpace(cardHolder))
            {
                TempData["PendingBooking"] = json;
                TempData["PaymentError"] = "Please fill in all card details correctly.";
                ViewBag.TourTitle = data.TourTitle;
                ViewBag.TotalPrice = data.TotalPrice;
                ViewBag.NumberOfPeople = data.NumberOfPeople;
                ViewBag.TourId = data.TourId;
                return View();
            }

            // Save booking
            var booking = new Booking
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                TourId = data.TourId,
                NumberOfPeople = data.NumberOfPeople,
                TotalPrice = data.TotalPrice,
                Status = BookingStatus.Confirmed
            };
            await _bookingService.CreateAsync(booking);

            // Save payment record
            var payment = new Payment
            {
                BookingId = booking.Id,
                Amount = data.TotalPrice,
                Method = "Card",
                Status = "Paid",
                TransactionId = "TXN-" + Guid.NewGuid().ToString("N")[..12].ToUpper()
            };
            await _paymentService.CreateAsync(payment);

            TempData["BookingSuccess_Title"] = data.TourTitle;
            TempData["BookingSuccess_Price"] = data.TotalPrice.ToString("F2");
            TempData["BookingSuccess_People"] = data.NumberOfPeople.ToString();
            TempData["BookingSuccess_Id"] = booking.Id.ToString();
            TempData["BookingSuccess_Card"] = "**** **** **** " + cleanCard[^4..];

            return RedirectToAction(nameof(Success));
        }

        // GET: /Payment/Success
        public IActionResult Success()
        {
            if (TempData["BookingSuccess_Id"] == null)
                return RedirectToAction("Index", "Tours");

            ViewBag.TourTitle = TempData["BookingSuccess_Title"];
            ViewBag.TotalPrice = TempData["BookingSuccess_Price"];
            ViewBag.NumberOfPeople = TempData["BookingSuccess_People"];
            ViewBag.BookingId = TempData["BookingSuccess_Id"];
            ViewBag.CardMask = TempData["BookingSuccess_Card"];

            return View();
        }
    }

    public class PendingBookingData
    {
        public int TourId { get; set; }
        public string TourTitle { get; set; } = string.Empty;
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
    }
}