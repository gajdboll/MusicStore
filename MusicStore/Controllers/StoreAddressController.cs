
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class StoreAddressController : Controller
    {
        private readonly MusicStoreContext _context;

        public StoreAddressController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Store ddress
        public async Task<IActionResult> Index()
        {
            return View(await _context.MusicStoreAddress
                                 .Where(x => x.isActive)
                                 .Include(pc => pc.OpeningHours)
                                 .ToListAsync());
        }

        // GET: Store ddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.MusicStoreAddress
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Store ddress/Create
        public IActionResult Create()
        {
            return View(new MusicStoreAddress());
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(MusicStoreAddress color)
        {
            if (ModelState.IsValid)
            {
                if (_context.MusicStoreAddress.Any(m => m.Position == color.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(color);
                }
                color.DateAdded = DateTime.Now;
                color.ModificationDate = DateTime.Now;
                _context.MusicStoreAddress.Add(color);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service got added";
                return RedirectToAction(nameof(Index));

            }
            return View(color);
        }

        // GET: Store ddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.MusicStoreAddress.FindAsync(id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(color);
        }

        // POST: Store ddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MusicStoreAddress color)
        {
            if (id != color.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.MusicStoreAddress
                        .AnyAsync(c => c.Position == color.Position && c.Id != color.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(color);
                }

                try
                {
                    var existingTop = await _context.MusicStoreAddress.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }

                    // update all the fields
                    existingTop.Name = color.Name;
                    existingTop.Descriptions = color.Descriptions;
                    existingTop.isActive = color.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = color.Position;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Service got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(color.Id))
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
            return View(color);
        }

        // GET: Store ddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.MusicStoreAddress
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(color);
        }

        // POST: Store ddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _context.MusicStoreAddress.FindAsync(id);
            if (color != null)
            {
                color.isActive = false;
                color.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Service got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.MusicStoreAddress.Any(e => e.Id == id);
        }
    }
}
