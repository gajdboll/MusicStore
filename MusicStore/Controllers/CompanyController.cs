using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.Users;
using MusicUtilities;

namespace MusicStore.Controllers
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: company
        public IActionResult Index()
        {
            return View(_unitOfWork.Company.GetAll().ToList());
        }

        // GET: company/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var company = _unitOfWork.Company.GetWithCompany(id);
            if (company == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(company);
        }

        // GET: company/Upsert
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            return View(_unitOfWork.Company.Get(u => u.Id == id));
        }

        // POST: company/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Company company, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                 if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string wwwRoot = "C:/xampp/htdocs/Images/";
                    string headerPath = Path.Combine(wwwRoot, "png", "company");

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
                        company.CompanyPicUrl = @"http://localhost/Images/png/company/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(company);
                    }
                }
                else
                {
                    if (company.Id != 0)
                    {
                        var existingcompany = _unitOfWork.Company.Get(p => p.Id == company.Id);
                        if (existingcompany != null)
                        {
                            company.CompanyPicUrl = existingcompany.CompanyPicUrl;
                        }
                    }
                }
                try
                {
                    if (company.Id == 0)
                    {
                        if (_unitOfWork.Company.IsPositionInUse(company))
                        {
                            TempData["error"] = "Position already in use";
                            return View(company);
                        }
                        company.DateAdded = DateTime.Now;
                        company.ModificationDate = DateTime.Now;
                        _unitOfWork.Company.Add(company);
                        TempData["Success"] = "Company got Created";
                    }
                    else
                    {
                        if (_unitOfWork.Company.IsPositionInUse(company))
                        {
                            TempData["error"] = "Position already in use";
                            return View(company);
                        }
                        company.ModificationDate = DateTime.Now;
                        _unitOfWork.Company.Update(company);
                        TempData["Success"] = "Company got Updated";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                    return View(company);
                }
            }
            return View(company);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            CompanyToBeDeleted.isActive = false;
            CompanyToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.Company.Update(CompanyToBeDeleted); 
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}