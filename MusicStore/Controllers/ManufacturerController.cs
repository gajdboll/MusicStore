using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Repository;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.CMS;
using MusicStoreData.Models.Users;
using MusicUtilities;

namespace MusicStore.Controllers
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ManufacturerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManufacturerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: manufacturer
        public IActionResult Index()
        {
            return View(_unitOfWork.Manufacturer.GetAll().ToList());
        }

        // GET: manufacturer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var manufacturer = _unitOfWork.Manufacturer.GetWithManufacturer(id);
            if (manufacturer == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(manufacturer);
        }

        // GET: manufacturer/Upsert
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Manufacturer());
            }
            return View(_unitOfWork.Manufacturer.Get(u => u.Id == id));
        }

        // POST: manufacturer/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Manufacturer man, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string wwwRoot = "C:/xampp/htdocs/Images/";
                    string headerPath = Path.Combine(wwwRoot, "png", "manufacture");

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
                        man.LogoUrl = @"http://localhost/Images/png/manufacture/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(man);
                    }
                }
                else
                {
                    if (man.Id != 0)
                    {
                        var existingcompany = _unitOfWork.Manufacturer.Get(p => p.Id == man.Id);
                        if (existingcompany != null)
                        {
                            man.LogoUrl = existingcompany.LogoUrl;
                        }
                    }
                }
                try
                {
                    if (man.Id == 0)
                    {
                        if (_unitOfWork.Manufacturer.IsPositionInUse(man))
                        {
                            TempData["error"] = "Position already in use";
                            return View(man);
                        }
                        man.DateAdded = DateTime.Now;
                        man.ModificationDate = DateTime.Now;
                        _unitOfWork.Manufacturer.Add(man);
                        TempData["Success"] = "Manufacturer got Created";
                    }
                    else
                    {
                        if (_unitOfWork.Manufacturer.IsPositionInUse(man))
                        {
                            TempData["error"] = "Position already in use";
                            return View(man);
                        }
                        man.ModificationDate = DateTime.Now;
                        _unitOfWork.Manufacturer.Update(man);
                        TempData["Success"] = "Manufacturer got Updated";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                    return View(man);
                }
            }
            return View(man);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Manufacturer> objManufacturerList = _unitOfWork.Manufacturer.GetAll().ToList();
            return Json(new { data = objManufacturerList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var ManufacturerToBeDeleted = _unitOfWork.Manufacturer.Get(u => u.Id == id);
            if (ManufacturerToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            ManufacturerToBeDeleted.isActive = false;
            ManufacturerToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.Manufacturer.Update(ManufacturerToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
 