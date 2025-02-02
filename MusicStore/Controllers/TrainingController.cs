
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class TrainingController : Controller
    {
        private readonly MusicStoreContext _context;

        public TrainingController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Training
        public async Task<IActionResult> Index()
        {
            return View(await _context.Training.ToListAsync());
        }

        // GET: Training/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Training/Create
        public IActionResult Create()
        {
            return View(new Training());
        }

        // POST: Training/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(Training training)
        {
            if (ModelState.IsValid)
            {
                if (_context.Training.Any(m => m.Position == training.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(training);
                }
                training.DateAdded = DateTime.Now;
                training.ModificationDate = DateTime.Now;
                _context.Training.Add(training);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Training got added";
                return RedirectToAction(nameof(Index));

            }
            return View(training);
        }

        // GET: Training/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var training = await _context.Training.FindAsync(id);
            if (training == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(training);
        }

        // POST: Training/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,isActive,Name,Position,Descriptions,DateAdded,ModificationDate,DateOfDelete")] Training training)
        {
            if (id != training.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.Training
                        .AnyAsync(c => c.Position == training.Position && c.Id != training.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(training);
                }

                try
                {
                    var existingTop = await _context.Training.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }

                    // update all the fields
                    existingTop.Name = training.Name;
                    existingTop.Descriptions = training.Descriptions;
                    existingTop.isActive = training.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = training.Position;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Training got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
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
            return View(training);
        }

        // GET: Training/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var training = await _context.Training
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(training);
        }

        // POST: Training/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.Training.FindAsync(id);
            if (training != null)
            {
                training.isActive = false;
                training.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Training got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.Id == id);
        }
    }
}
