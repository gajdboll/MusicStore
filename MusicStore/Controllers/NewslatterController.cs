
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class NewslatterController : Controller
    {
        private readonly MusicStoreContext _context;

        public NewslatterController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Newslatter
        public async Task<IActionResult> Index()
        {
            return View(await _context.Newslatter.ToListAsync());
        }

        // GET: Newslatter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Newslatter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Newslatter/Create
        public IActionResult Create()
        {
            return View(new Newslatter());
        }

        // POST: Newslatter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Newslatter color)
        {
            if (ModelState.IsValid)
            {
                if (_context.Newslatter.Any(m => m.Position == color.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(color);
                }
                color.DateAdded = DateTime.Now;
                color.ModificationDate = DateTime.Now;
                _context.Newslatter.Add(color);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Newslatter got added";
                return RedirectToAction(nameof(Index));

            }
            return View(color);
        }

        // GET: Newslatter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.Newslatter.FindAsync(id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(color);
        }

        // POST: Newslatter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Newslatter color)
        {
            if (id != color.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.Newslatter
                        .AnyAsync(c => c.Position == color.Position && c.Id != color.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(color);
                }

                try
                {
                    var existingTop = await _context.Newslatter.FindAsync(id);

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
                    TempData["Success"] = "Newslatter got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewslatterExists(color.Id))
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

        // GET: Newslatter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.Newslatter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(color);
        }

        // POST: Newslatter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _context.Newslatter.FindAsync(id);
            if (color != null)
            {
                color.isActive = false;
                color.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Newslatter got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool NewslatterExists(int id)
        {
            return _context.Newslatter.Any(e => e.Id == id);
        }
    }
}
