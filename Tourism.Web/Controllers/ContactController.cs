using Microsoft.AspNetCore.Mvc;
using Tourism.Data.Models.Entities;
using Tourism.Services;

namespace Tourism.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactService;

        public ContactController(IContactMessageService contactService)
        {
            _contactService = contactService;
        }

        // GET: /Contact
        public IActionResult Index() => View();

        // POST: /Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactMessage model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _contactService.CreateAsync(model);
            TempData["Success"] = "Your message has been sent! We'll get back to you within 24 hours.";
            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success() => View();
    }
}