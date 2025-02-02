using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.Store;

namespace MusicStore.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductCategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product 
        public async Task<IActionResult> Index()
        {
            return View(_unitOfWork.ProductCategory.GetAll(
                m => m.isActive,
                include: q => q.Include(pc => pc.WebHeader)
            ));
        }

        // GET: ProductCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var productCategory = _unitOfWork.ProductCategory.GetWithWebHeaders((int)id);
            if (productCategory == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategory/Create & Edit
        public IActionResult Upsert(int? id)
        {
            ViewData["WebHeaderId"] = new SelectList(_unitOfWork.WebHeader.GetAll(), "Id", "Name");
            if (id == null || id == 0)
            {
                return View(new ProductCategory());
            }
            return View(_unitOfWork.ProductCategory.Get(u => u.Id == id));
        }

        // POST: ProductCategory/Create & Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductCategory category, IFormFile? file)
        {
            ViewData["WebHeaderId"] = new SelectList(_unitOfWork.ProductSubCategory.GetAll(), "Id", "Name");

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string wwwRoot = "C:/xampp/htdocs/Images/";
                    string headerPath = Path.Combine(wwwRoot, "png", "category");

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
                        category.CategoryImageUrl = @"http://localhost/Images/png/category/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(category);
                    }
                }
                else
                {
                    if (category.Id != 0)
                    {
                        var existingcompany = _unitOfWork.ProductCategory.Get(p => p.Id == category.Id);
                        if (existingcompany != null)
                        {
                            category.CategoryImageUrl = existingcompany.CategoryImageUrl;
                        }
                    }
                }
                try
                {
                    if (category.Id == 0)
                    {
                        if (_unitOfWork.ProductCategory.IsPositionInUse(category))
                        {
                            TempData["error"] = "Position already in use";
                            return View(category);
                        }
                        category.DateAdded = DateTime.Now;
                        category.ModificationDate = DateTime.Now;
                        _unitOfWork.ProductCategory.Add(category);
                        TempData["Success"] = "Product Category got Created";
                    }
                    else
                    {
                        if (_unitOfWork.ProductCategory.IsPositionInUse(category))
                        {
                            TempData["error"] = "Position already in use";
                            return View(category);
                        }
                        category.ModificationDate = DateTime.Now;
                        _unitOfWork.ProductCategory.Update(category);
                        TempData["Success"] = "Product Category got Updated";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                    return View(category);
                }
            }
            ViewBag.WebHeaderId = new SelectList(_unitOfWork.WebHeader.GetAll(), "Id", "Name");

            return View(category);
        }

        // GET: ProductCategory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var productCategory = _unitOfWork.ProductCategory.GetWithWebHeaders((int)id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productCategories = _unitOfWork.ProductCategory.GetAll(
                include: q => q.Include(pc => pc.WebHeader)
            )
            .Select(pc => new
            {
                pc.Id,
                pc.Name,
                pc.Descriptions,
                pc.isActive,
                WebHeader = new
                {
                    pc.WebHeader.Name
                }
            }).ToList();


            return Json(new { data = productCategories });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.ProductCategory.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            productToBeDeleted.isActive = false;
            productToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.ProductCategory.Update(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
