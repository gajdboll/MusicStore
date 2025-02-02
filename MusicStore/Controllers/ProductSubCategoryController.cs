
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Controllers
{

    public class ProductSubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductSubCategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductSubCategory
        public IActionResult Index()
        {
            return View(_unitOfWork.ProductSubCategory.GetAll(m => m.isActive,
                include: q => q.Include(m => m.ProductCategory)).ToList());
        }

        // GET: ProductSubCategory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var productSubCategory = _unitOfWork.ProductSubCategory.GetWithCategory(id);
            if (productSubCategory == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(productSubCategory);
        }

        // GET: ProductSubCategory/Create
        public IActionResult Upsert(int? id)
        {
            ViewData["ProductCategoryId"] = new SelectList(
                _unitOfWork.ProductCategory.GetAll(
                    include: q => q.Include(psc => psc.WebHeader)
                ), "Id", "Name");

            if (id == null || id == 0)
            {
                return View();
            }
            return View(_unitOfWork.ProductSubCategory.Get(u => u.Id == id));
        }

        // POST: ProductSubCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductSubCategory productSubCategory, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string wwwRoot = "C:/xampp/htdocs/Images/";
                    string headerPath = Path.Combine(wwwRoot, "png", "subcategory");


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
                        productSubCategory.SubCategoryImageUrl = @"http://localhost/Images/png/category/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving file. Please try again.");
                        return View(productSubCategory);
                    }
                }
                else
                {
                    if (productSubCategory.Id != 0)
                    {
                        var existingProductSubCategory = _unitOfWork.ProductSubCategory.Get(p => p.Id == productSubCategory.Id);
                        if (existingProductSubCategory != null)
                        {
                            productSubCategory.SubCategoryImageUrl = existingProductSubCategory.SubCategoryImageUrl;
                        }
                    }
                }
                try
                {
                    if (productSubCategory.Id == 0)
                    {
                        if (_unitOfWork.ProductSubCategory.IsPositionInUse(productSubCategory))
                        {
                            TempData["error"] = "Position already in use so it got changed";
                            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.ProductCategory.GetAll(), "Id", "Name", productSubCategory.ProductCategoryId);
                            return RedirectToAction(nameof(Index));
                        }
                        productSubCategory.DateAdded = DateTime.Now;
                        productSubCategory.ModificationDate = DateTime.Now;
                        _unitOfWork.ProductSubCategory.Add(productSubCategory);
                        TempData["Success"] = "Category got Created";
                    }
                    else
                    {
                        if (_unitOfWork.ProductSubCategory.IsPositionInUse(productSubCategory))
                        {
                            TempData["error"] = "Position already in use so it got changed";
                            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.ProductCategory.GetAll(), "Id", "Name", productSubCategory.ProductCategoryId);
                            return RedirectToAction(nameof(Index));
                        }
                        productSubCategory.ModificationDate = DateTime.Now;
                        //productSubCategory.Position += 1;
                        _unitOfWork.ProductSubCategory.Update(productSubCategory);
                        TempData["Success"] = "Category got Updated";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                    ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.ProductCategory.GetAll(), "Id", "Name", productSubCategory.ProductCategoryId);
                    return View(productSubCategory);
                }
            }
            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.ProductCategory.GetAll(), "Id", "Name", productSubCategory.ProductCategoryId);
            return View(productSubCategory);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.ProductSubCategory.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            productToBeDeleted.isActive = false;
            productToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.ProductSubCategory.Update(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Successful Delete" });
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productSubCategories = _unitOfWork.ProductSubCategory.GetAll(
            include: q => q.Include(psc => psc.ProductCategory))
                .Select(pc => new
                {
                    pc.Id,
                    pc.Name,
                    pc.Descriptions,
                    pc.isActive,
                    ProductCategory = new
                    {
                        pc.ProductCategory.Name
                    }
                }).ToList();

            return Json(new { data = productSubCategories });

        }
        #endregion

    }
}
