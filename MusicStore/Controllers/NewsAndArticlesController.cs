using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{


    public class NewsAndArticleController : Controller
    {
        private readonly MusicStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsAndArticleController(MusicStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: NewsAndArticle
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsAndArticles.ToListAsync());
        }

        // GET: NewsAndArticle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsAndArticles = await _context.NewsAndArticles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsAndArticles == null)
            {
                return NotFound();
            }

            return View(newsAndArticles);
        }

        // GET: NewsAndArticle/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsAndArticle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( NewsAndArticles newsAndArticles, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string headerPath = Path.Combine(wwwRoot, "png", "news");

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
                        newsAndArticles.BlogImage = Path.Combine("..", "..", "png", "news", fileName);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(newsAndArticles);
                    }
                }

                try
                {
                    newsAndArticles.DateAdded = DateTime.Now;
                    newsAndArticles.ModificationDate = DateTime.Now;
                    _context.Add(newsAndArticles);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "News And Articles was added successfully.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                }
            }
            if (User.IsInRole("Admin"))
            {
                return View(newsAndArticles);
            }
            else
            {
                return RedirectToAction("ShowBlogs", "Home");
            }
        }


        // GET: NewsAndArticle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsAndArticles = await _context.NewsAndArticles.FindAsync(id);
            if (newsAndArticles == null)
            {
                return NotFound();
            }
            return View(newsAndArticles);
        }

        // POST: NewsAndArticle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  NewsAndArticles newsAndArticles, IFormFile? file)
        {
            if (id != newsAndArticles.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Pobierz aktualny stan webHeaders z bazy danych
                    var currentWebHeader = await _context.NewsAndArticles.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
                    if (currentWebHeader == null)
                    {
                        TempData["error"] = "WebHeader not found";
                        return NotFound();
                    }

                    if (file != null && file.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(currentWebHeader.BlogImage))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png\\news", currentWebHeader.BlogImage);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\png", "news");
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

                        newsAndArticles.BlogImage = @"../.././png/news/" + fileName;
                    }
                    newsAndArticles.ModificationDate = DateTime.Now;
                    _context.Update(newsAndArticles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsAndArticlesExists(newsAndArticles.Id))
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "News And Articles got Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(newsAndArticles);
        }


        // GET: NewsAndArticle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsAndArticles = await _context.NewsAndArticles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsAndArticles == null)
            {
                return NotFound();
            }

            return View(newsAndArticles);
        }

        // POST: NewsAndArticle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsAndArticles = await _context.NewsAndArticles.FindAsync(id);
            if (newsAndArticles != null)
            {
                newsAndArticles.isActive = false;
                newsAndArticles.DateOfDelete = DateTime.Now;
             }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsAndArticlesExists(int id)
        {
            return _context.NewsAndArticles.Any(e => e.Id == id);
        }
    }
}
