
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class ServicesController : Controller
    {
        private readonly MusicStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicesController(MusicStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Service
        public async Task<IActionResult> Index()
        {
            return View(await _context.Service.Where(x => x.isActive).ToListAsync());
        }

        // GET: Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (more == null)
            {
                return NotFound();
            }

            return View(more);
        }

        // GET: Concert/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concert/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string headerPath = Path.Combine(wwwRoot, @"png\Concert");

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
                        service.PictureUrl = @"../.././png/Concert/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(service);
                    }
                }

                try
                {
                    service.DateAdded = DateTime.Now;
                    service.ModificationDate = DateTime.Now;
                    _context.Add(service);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Concert items got Added";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                }
            }
            TempData["Success"] = "Concert Items got Added";

            return View(service);

        }

        // GET: service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var more = await _context.Service.FindAsync(id);
            if (more == null)
            {
                return NotFound();
            }
            return View(more);
        }

        // POST: service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service, IFormFile? file)
        {
            if (id != service.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var currentWebHeader = await _context.Service.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
                    if (currentWebHeader == null)
                    {
                        TempData["error"] = "service not found";
                        return NotFound();
                    }

                    if (file != null && file.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(currentWebHeader.PictureUrl))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png\\service", currentWebHeader.PictureUrl);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png", "service");
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
                        service.PictureUrl = @"../.././png/service/" + fileName;
                    }
                    service.ModificationDate = DateTime.Now;
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "service Items got Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Concert = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Concert == null)
            {
                return NotFound();
            }

            return View(Concert);
        }

        // POST: service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var more = await _context.Service.FindAsync(id);
            if (more != null)
            {
                more.isActive = false;
                more.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.Id == id);
        }
    }
}
