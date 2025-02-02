using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.CMS;
 using MusicUtilities;

namespace MusicStore.Controllers
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class WebHeadersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WebHeadersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: web
        public IActionResult Index()
        {
            return View(_unitOfWork.WebHeader.GetAll().ToList());
        }

        // GET: web/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var web = _unitOfWork.WebHeader.GetWithWebHeaders(id);
            if (web == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(web);
        }

        // GET: web/Upsert
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new WebHeaders());
            }
             return View(_unitOfWork.WebHeader.Get(u => u.Id == id));
        }

        // POST: web/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(WebHeaders web, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string headerPath = Path.Combine(wwwRoot, "png", "web");

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
                        web.ImageUrl = @"../.././png/web/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(web);
                    }
                }
                else
                {
                    if (web.Id != 0)
                    {
                        var existingweb = _unitOfWork.WebHeader.Get(p => p.Id == web.Id);
                        if (existingweb != null)
                        {
                            web.ImageUrl = existingweb.ImageUrl;
                        }
                    }
                }
                try
                {
                    if (web.Id == 0)
                    {
                        if (_unitOfWork.WebHeader.IsPositionInUse(web))
                        {
                            TempData["error"] = "Position already in use";
                            return View(web);
                        }
                        web.DateAdded = DateTime.Now;
                        web.ModificationDate = DateTime.Now;
                        _unitOfWork.WebHeader.Add(web);
                        TempData["Success"] = "WebHeader got Created";
                    }
                    else
                    {
                        if (_unitOfWork.WebHeader.IsPositionInUse(web))
                        {
                            TempData["error"] = "Position already in use";
                            return View(web);
                        }
                        web.ModificationDate = DateTime.Now;
                        _unitOfWork.WebHeader.Update(web);
                        TempData["Success"] = "WebHeader got Updated";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                    return View(web);
                }
            }
            return View(web);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<WebHeaders> objWebHeaderList = _unitOfWork.WebHeader.GetAll().ToList();
            return Json(new { data = objWebHeaderList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var WebHeaderToBeDeleted = _unitOfWork.WebHeader.Get(u => u.Id == id);
            if (WebHeaderToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            WebHeaderToBeDeleted.isActive = false;
            WebHeaderToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.WebHeader.Update(WebHeaderToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}