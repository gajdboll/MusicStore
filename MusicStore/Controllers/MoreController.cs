
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class MoreController : Controller
    {
        private readonly MusicStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MoreController(MusicStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: TermsAndCondition
        public async Task<IActionResult> Index()
        {
            return View(await _context.More.ToListAsync());
        }

        // GET: TermsAndCondition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.More
                .FirstOrDefaultAsync(m => m.Id == id);
            if (more == null)
            {
                return NotFound();
            }

            return View(more);
        }

        // GET: TermsAndCondition/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TermsAndCondition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(More more, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string headerPath = Path.Combine(wwwRoot, @"png\more");

                    if (!Directory.Exists(headerPath))
                    {
                        Directory.CreateDirectory(headerPath);
                    }

                    string filePath = Path.Combine(headerPath, fileName);

                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        more.MoreIcon = @"../.././png/more/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(more);
                    }
                }

                try
                {
                    more.DateAdded = DateTime.Now;
                    more.ModificationDate = DateTime.Now;
                    _context.Add(more);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "More items got Added";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                }
            }
            TempData["Success"] = "More Items got Added";

            return View(more);

        }

        // GET: TermsAndCondition/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.More.FindAsync(id);
            if (more == null)
            {
                return NotFound();
            }
            return View(more);
        }

        // POST: TermsAndCondition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, More more, IFormFile? file)
        {
            if (id != more.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var currentWebHeader = await _context.More.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
                    if (currentWebHeader == null)
                    {
                        TempData["error"] = "T&C not found";
                        return NotFound();
                    }

                    if (file != null && file.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(currentWebHeader.MoreIcon))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png\\more", currentWebHeader.MoreIcon);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png", "more");
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Path.GetRandomFileName().Substring(0, 8) + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploads, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        more.MoreIcon = @"../.././png/more/" + fileName;
                    }
                    more.ModificationDate = DateTime.Now;
                    _context.Update(more);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoreExists(more.Id))
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "More Items got Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(more);
        }

        // GET: more/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.More
                .FirstOrDefaultAsync(m => m.Id == id);
            if (more == null)
            {
                return NotFound();
            }

            return View(more);
        }

        // POST: more/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var more = await _context.More.FindAsync(id);
            if (more != null)
            {
                more.isActive = false;
                more.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoreExists(int id)
        {
            return _context.More.Any(e => e.Id == id);
        }
    }
}
