using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{
    public class SubscriberEmailController : Controller
    {
        private readonly MusicStoreContext _context;

        public SubscriberEmailController(MusicStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(email))
            {
                var newSubscriber = new Subscribers
                {
                    Name = email,
                    DateAdded = DateTime.Now
                };

                _context.Subscribers.Add(newSubscriber);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Subscription successful!" });
            }

            return Json(new { success = false, message = "Please enter a valid email." });
        }
    }
}