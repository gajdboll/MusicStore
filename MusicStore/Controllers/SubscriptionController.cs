using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly MusicStoreContext _context;

        public  SubscriptionController (MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Subscription
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subscription.ToListAsync());
        }

        // GET: Subscription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscription/Create
        public IActionResult Create()
        {
            return View(new Subscription());
        }

        // POST: Subscription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                if (_context.Subscription.Any(m => m.Position == subscription.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(subscription);
                }
                subscription.DateAdded = DateTime.Now;
                subscription.ModificationDate = DateTime.Now;
                _context.Subscription.Add(subscription);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Subscription got added";
                return RedirectToAction(nameof(Index));

            }
            return View(subscription);
        }

        // GET: Subscription/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var subscription = await _context.Subscription.FindAsync(id);
            if (subscription == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(subscription);
        }

        // POST: Subscription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,isActive,Name,Position,Descriptions,DateAdded,ModificationDate,DateOfDelete")] Subscription subscription)
        {
            if (id != subscription.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.Subscription
                        .AnyAsync(c => c.Position == subscription.Position && c.Id != subscription.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(subscription);
                }

                try
                {
                    var existingTop = await _context.Subscription.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }

                    // update all the fields
                    existingTop.Name = subscription.Name;
                    existingTop.Descriptions = subscription.Descriptions;
                    existingTop.isActive = subscription.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = subscription.Position;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Subscription got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }

        // GET: Subscription/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var subscription = await _context.Subscription
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _context.Subscription.FindAsync(id);
            if (subscription != null)
            {
                subscription.isActive = false;
                subscription.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Subscription got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
            return _context.Subscription.Any(e => e.Id == id);
        }
    }
}
