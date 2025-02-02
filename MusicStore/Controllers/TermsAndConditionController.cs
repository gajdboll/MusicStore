
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Repository;
using MusicStore.Repository.IRepository;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{

    public class TermsAndConditionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MusicStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TermsAndConditionController(IUnitOfWork unitOfWork, MusicStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: TermsAndCondition
        public async Task<IActionResult> Index()
        {
            return View(await _context.TermsAndCondition.ToListAsync());
        }

        // GET: TermsAndCondition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termsAndCondition = await _context.TermsAndCondition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termsAndCondition == null)
            {
                return NotFound();
            }

            return View(termsAndCondition);
        }
 
        // POST: T&C/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TermsAndCondition web, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string headerPath = Path.Combine(wwwRoot, "png", "terms");

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
                        web.TermsPhotoUrl = @"../.././png/terms/" + fileName;
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
                        var existingcompany = _unitOfWork.TermsAndCondition.Get(p => p.Id == web.Id);
                        if (existingcompany != null)
                        {
                            web.TermsPhotoUrl = existingcompany.TermsPhotoUrl;
                        }
                    }
                }
                try
                {
                    if (web.Id == 0)
                    {
                        if (_unitOfWork.TermsAndCondition.IsPositionInUse(web))
                        {
                            TempData["error"] = "Position already in use";
                            return View(web);
                        }
                        web.DateAdded = DateTime.Now;
                        web.ModificationDate = DateTime.Now;
                        _unitOfWork.TermsAndCondition.Add(web);
                        TempData["Success"] = "T&C got Created";
                    }
                    else
                    {
                        if (_unitOfWork.TermsAndCondition.IsPositionInUse(web))
                        {
                            TempData["error"] = "Position already in use";
                            return View(web);
                        }
                        web.ModificationDate = DateTime.Now;
                        _unitOfWork.TermsAndCondition.Update(web);
                        TempData["Success"] = "TermsAndCondition got Updated";
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
            List<TermsAndCondition> objCompanyList = _unitOfWork.TermsAndCondition.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var TermsAndConditionToBeDeleted = _unitOfWork.TermsAndCondition.Get(u => u.Id == id);
            if (TermsAndConditionToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            TermsAndConditionToBeDeleted.isActive = false;
            TermsAndConditionToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.TermsAndCondition.Update(TermsAndConditionToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}