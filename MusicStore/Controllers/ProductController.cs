using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.Store;


namespace MusicStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product 
        public async Task<IActionResult> Index()
        {
            return View(_unitOfWork.Product.GetAll(
                m => m.isActive,
                include: q => q.Include(pc => pc.Color)
                .Include(p => p.ProductType)
                .ThenInclude(p => p.ProductSubCategory)
                .ThenInclude(p => p.ProductCategory)
            ));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var product = _unitOfWork.Product.GetWithColor((int)id);
            if (product == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create & Edit
        public IActionResult Upsert(int? id)
        {
            ViewData["ColorId"] = new SelectList(_unitOfWork.ProductSubCategory.GetAll(), "Id", "Name");
            if (id == null || id == 0)
            {
                return View(new Product());
            }
            return View(_unitOfWork.Product.Get(u => u.Id == id));
        }

        // POST: Product/Create & Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Product product )
        {
            ViewData["ProductTypeId"] = new SelectList(_unitOfWork.ProductSubCategory.GetAll(), "Id", "Name");

            if (ModelState.IsValid)
            { 
                try
                {
                    if (product.Id == 0)
                    {
                        if (_unitOfWork.Product.IsPositionInUse(product))
                        {
                            TempData["error"] = "Position already in use";
                            return View(product);
                        }
                        product.DateAdded = DateTime.Now;
                        product.ModificationDate = DateTime.Now;
                        _unitOfWork.Product.Add(product);
                        TempData["Success"] = "Product Category got Created";
                    }
                    else
                    {
                        if (_unitOfWork.Product.IsPositionInUse(product))
                        {
                            TempData["error"] = "Position already in use";
                            return View(product);
                        }
                        product.ModificationDate = DateTime.Now;
                        _unitOfWork.Product.Update(product);
                        TempData["Success"] = "Product Category got Updated";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database. Please try again.");
                    return View(product);
                }
            }
            ViewBag.ProductTypeId = new SelectList(_unitOfWork.ProductType.GetAll(), "Id", "Name");

            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.GetWithColor((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productCategories = _unitOfWork.Product.GetAll(
                include: q => q.Include(pc => pc.Color)
                .Include(pc => pc.ProductType)
                .ThenInclude(pc => pc.ProductSubCategory)
                .ThenInclude(pc => pc.ProductCategory)
            )
            .Select(pc => new
            {
                pc.Id,
                pc.Name,
                pc.Descriptions,
                pc.isActive,
                Color = new
                {
                    pc.Color.Name
                },
                ProductType = new
                { 
                    p = pc.ProductType.Name
                }
            }).ToList();


            return Json(new { data = productCategories });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            productToBeDeleted.isActive = false;
            productToBeDeleted.DateOfDelete = DateTime.Now;
            _unitOfWork.Product.Update(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
