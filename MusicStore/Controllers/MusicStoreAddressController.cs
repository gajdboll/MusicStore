using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{
    public class MusicStoreAddressController : Controller
    {
        private readonly MusicStoreContext _context;

        public MusicStoreAddressController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: MusicStoreAddress
        public async Task<IActionResult> Index()
        {
            return View(await _context.MusicStoreAddress.ToListAsync());
        }

        // GET: MusicStoreAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicStoreAddress = await _context.MusicStoreAddress
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicStoreAddress == null)
            {
                return NotFound();
            }

            return View(musicStoreAddress);
        }

        // GET: MusicStoreAddress/Create
        public IActionResult Create()
        {
            return View(new MusicStoreAddress());
        }

        // POST: MusicStoreAddress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicStoreAddress musicStoreAddress)
        {
            if (ModelState.IsValid)
            {
                if (_context.MusicStoreAddress.Any(m => m.Position == musicStoreAddress.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(musicStoreAddress);
                }
                musicStoreAddress.DateAdded = DateTime.Now;
                musicStoreAddress.ModificationDate = DateTime.Now;
                _context.MusicStoreAddress.Add(musicStoreAddress);
                await _context.SaveChangesAsync();
                TempData["Success"] = "MusicStoreAddress got added";
                return RedirectToAction(nameof(Index));

            }
            return View(musicStoreAddress);
        }

        // GET: MusicStoreAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var musicStoreAddress = await _context.MusicStoreAddress.FindAsync(id);
            if (musicStoreAddress == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(musicStoreAddress);
        }

        // POST: MusicStoreAddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,isActive,Name,Position,Descriptions,DateAdded,ModificationDate,DateOfDelete")] MusicStoreAddress musicStoreAddress)
        {
            if (id != musicStoreAddress.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.MusicStoreAddress
                        .AnyAsync(c => c.Position == musicStoreAddress.Position && c.Id != musicStoreAddress.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(musicStoreAddress);
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
                    existingTop.Name = musicStoreAddress.Name;
                    existingTop.Descriptions = musicStoreAddress.Descriptions;
                    existingTop.isActive = musicStoreAddress.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = musicStoreAddress.Position;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "MusicStoreAddress got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicStoreAddressExists(musicStoreAddress.Id))
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
            return View(musicStoreAddress);
        }

        // GET: MusicStoreAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var musicStoreAddress = await _context.MusicStoreAddress
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicStoreAddress == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(musicStoreAddress);
        }

        // POST: MusicStoreAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicStoreAddress = await _context.MusicStoreAddress.FindAsync(id);
            if (musicStoreAddress != null)
            {
                musicStoreAddress.isActive = false;
                musicStoreAddress.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "MusicStoreAddress got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool MusicStoreAddressExists(int id)
        {
            return _context.MusicStoreAddress.Any(e => e.Id == id);
        }
    }
}

 
