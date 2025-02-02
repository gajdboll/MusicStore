
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
 
namespace MusicStore.Controllers
{

    public class DiscountCodeController : Controller
    {
        private readonly MusicStoreContext _context;

        public DiscountCodeController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: DiscountCode
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscountCode.ToListAsync());
        }

        // GET: DiscountCode/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.DiscountCode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // GET: DiscountCode/Create
        public IActionResult Create()
        {
            return View(new DiscountCode());
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(DiscountCode code)
        {
            if (ModelState.IsValid)
            {
                if (_context.DiscountCode.Any(m => m.Position == code.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(code);
                }
                code.DateAdded = DateTime.Now;
                code.ModificationDate = DateTime.Now;
                _context.DiscountCode.Add(code);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Discount Code got added";
                return RedirectToAction(nameof(Index));

            }
            return View(code);
        }

        // GET: DiscountCode/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var code = await _context.DiscountCode.FindAsync(id);
            if (code == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(code);
        }

        // POST: DiscountCode/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiscountCode code)
        {
            if (id != code.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.DiscountCode
                        .AnyAsync(c => c.Position == code.Position && c.Id != code.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(code);
                }

                try
                {
                    var existingTop = await _context.DiscountCode.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }

                    // update all the fields
                    existingTop.Name = code.Name;
                    existingTop.Descriptions = code.Descriptions;
                    existingTop.isActive = code.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = code.Position;
                    existingTop.DiscountPercent = code.DiscountPercent;
                    existingTop.ExpirationDate = code.ExpirationDate;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Discount Code got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCode(code.Id))
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
            return View(code);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var color = await _context.DiscountCode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(color);
        }

        // POST: DiscountCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var code = await _context.DiscountCode.FindAsync(id);
            if (code != null)
            {
                code.isActive = false;
                code.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Discount Code got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCode(int id)
        {
            return _context.DiscountCode.Any(e => e.Id == id);
        }
    }
}
