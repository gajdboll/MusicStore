using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;
 
namespace MusicStore.Controllers
{

    public class SocialMediaController : Controller
    {
        private readonly MusicStoreContext _context;

        public SocialMediaController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: SocialMedia
        public async Task<IActionResult> Index()
        {
            return View(await _context.SocialMedia.ToListAsync());
        }

        // GET: SocialMedia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            return View(socialMedia);
        }

        // GET: SocialMedia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SocialMedia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SocialMediaImageUrl,SocialMoreInfo,Id,isActive,Name,Position,Descriptions,DateAdded,ModificationDate,DateOfDelete")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                socialMedia.DateAdded = DateTime.Now;
                socialMedia.ModificationDate = DateTime.Now;
                _context.Add(socialMedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialMedia);
        }

        // GET: SocialMedia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedia.FindAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }
            return View(socialMedia);
        }

        // POST: SocialMedia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SocialMediaImageUrl,SocialMoreInfo,Id,isActive,Name,Position,Descriptions,DateAdded,ModificationDate,DateOfDelete")] SocialMedia socialMedia)
        {
            if (id != socialMedia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    socialMedia.ModificationDate = DateTime.Now;
                    _context.Update(socialMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialMediaExists(socialMedia.Id))
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
            return View(socialMedia);
        }

        // GET: SocialMedia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            return View(socialMedia);
        }

        // POST: SocialMedia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socialMedia = await _context.SocialMedia.FindAsync(id);
            if (socialMedia != null)
            {
                socialMedia.isActive = false;
                socialMedia.DateOfDelete = DateTime.Now;
             }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialMediaExists(int id)
        {
            return _context.SocialMedia.Any(e => e.Id == id);
        }
    }
}
