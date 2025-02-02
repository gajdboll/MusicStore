
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using NPOI.SS.Formula.Functions;

namespace MusicStore.Controllers
{

    public class FAQController : Controller
    {
        private readonly MusicStoreContext _context;

        public FAQController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Service
        public async Task<IActionResult> Index()
        {
            return View(await _context.FAQ.ToListAsync());
        }

        // GET: Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.FAQ
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: FAQ/Create
        public IActionResult Create()
        {
            return View(new FAQ());
        }

        // POST: FAQ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                if (_context.FAQ.Any(m => m.Position == faq.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(faq);
                }

                // Assign the ProductTypeId to the FAQ entity
                faq.ProductTypeId = 1;

                faq.Name = "Create_" + DateTime.Now;
                faq.DateAdded = DateTime.Now;
                faq.ModificationDate = DateTime.Now;
                faq.Answer = "N/A";

                _context.FAQ.Add(faq);
                await _context.SaveChangesAsync();

                TempData["Success"] = "FAQ got added";
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }


        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var faq = await _context.FAQ.FindAsync(id);
            if (faq == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(faq);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FAQ faq)
        {
            if (id != faq.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                bool positionInUse = await _context.FAQ
                        .AnyAsync(c => c.Position == faq.Position && c.Id != faq.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(faq);
                }

                try
                {
                    var existingTop = await _context.FAQ.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }

                    // update all the fields
                    existingTop.Name = "Update_"+DateTime.Now;
                    existingTop.Subject = faq.Subject;
                     existingTop.isActive = faq.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = faq.Position;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "FAQ got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQExists(faq.Id))
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
            return View(faq);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var faq = await _context.FAQ
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(faq);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faq = await _context.FAQ.FindAsync(id);
            if (faq != null)
            {
                faq.isActive = false;
                faq.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "FAQ got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool FAQExists(int id)
        {
            return _context.FAQ.Any(e => e.Id == id);
        }
    }
}
