
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class AboutUsController : Controller
    {
        private readonly MusicStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutUsController(MusicStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: TermsAndCondition
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutUs.ToListAsync());
        }

        // GET: TermsAndCondition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (more == null)
            {
                return NotFound();
            }

            return View(more);
        }

        // GET: TermsAndCondition/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TermsAndCondition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUs about, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string headerPath = Path.Combine(wwwRoot, @"png\about");

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
                        about.PhotoUrl = @"../.././png/about/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(about);
                    }
                }

                try
                {
                    about.DateAdded = DateTime.Now;
                    about.ModificationDate = DateTime.Now;
                    _context.Add(about);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "about items got Added";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                }
            }
            TempData["Success"] = "about Items got Added";

            return View(about);

        }

        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.AboutUs.FindAsync(id);
            if (more == null)
            {
                return NotFound();
            }
            return View(more);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutUs about, IFormFile? file)
        {
            if (id != about.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var current = await _context.AboutUs.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
                    if (current == null)
                    {
                        TempData["error"] = "T&C not found";
                        return NotFound();
                    }

                    if (file != null && file.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(current.PhotoUrl))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png\\about", current.PhotoUrl);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png", "about");
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
                        about.PhotoUrl = @"../.././png/about/" + fileName;
                    }
                    about.ModificationDate = DateTime.Now;
                    _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(about.Id))
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "AboutUs Items got Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AboutUs == null)
            {
                return NotFound();
            }

            return View(AboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var more = await _context.AboutUs.FindAsync(id);
            if (more != null)
            {
                more.isActive = false;
                more.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(int id)
        {
            return _context.AboutUs.Any(e => e.Id == id);
        }
    }
}
