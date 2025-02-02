
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;

namespace MusicStore.Controllers
{

    public class TechInfoController : Controller
    {
        private readonly MusicStoreContext _context;

        public TechInfoController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Color
        public async Task<IActionResult> Index()
        {
            return View(await _context.TechInfos.ToListAsync());
        }

        // GET: Color/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.TechInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: TechInfos/Create
        public IActionResult Create()
        {
            return View(new TechInfo());
        }

        // POST: TechInfos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(TechInfo color)
        {
            if (ModelState.IsValid)
            {
   
                _context.TechInfos.Add(color);
                await _context.SaveChangesAsync();
                TempData["Success"] = "TechInfos got added";
                return RedirectToAction(nameof(Index));

            }
            return View(color);
        }

        // GET: Color/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.TechInfos.FindAsync(id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(color);
        }

        // POST: TechInfos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TechInfo color)
        {
            if (id != color.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.TechInfos
                        .AnyAsync(c => c.Id != color.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(color);
                }

                try
                {
                    var existingTop = await _context.TechInfos.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }
 
                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Color got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechInfosExists(color.Id))
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

        // GET: TechInfos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.TechInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(color);
        }

        // POST: TechInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.SaveChangesAsync();
            TempData["Success"] = "TechInfos got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool TechInfosExists(int id)
        {
            return _context.TechInfos.Any(e => e.Id == id);
        }
    }
}
