using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.IRepository;
using MusicStore.ViewModels;
using MusicStoreData.Models.CMS;
using MusicStoreData.Models.Store;
using Stripe;
using System.Security.Claims;

namespace MusicStore.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
 
        // GET: ProductType
        public IActionResult Index()
        {
            //var productTypes = 

            return View(_unitOfWork.ProductType.GetAll(
                include: q => q
                    .Include(pt => pt.Manufacturer)
                    .Include(pt => pt.ProductSubCategory)).ToList());

        }


        // GET: ProductType/Upsert/5
        public IActionResult Upsert(int? id)
        {
            var productTypeVM = new ProductTypeVM
            {
                ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }),
                ProductSubCategoryList = _unitOfWork.ProductSubCategory.GetAll().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                ProductType = new ProductType
                {
                    ProductImages = new List<ProductImage>()
                }
            };

            if (id == null || id == 0)
            {
                // Create
                return View(productTypeVM);
            }
            else
            {
                // Update
                productTypeVM.ProductType = _unitOfWork.ProductType.Get(
                 p => p.Id == id,
                include: q => q
                    .Include(p => p.Manufacturer)
                .Include(p => p.ProductSubCategory)
                .Include(p => p.ProductImages));

                if (productTypeVM.ProductType == null)
                {
                    return NotFound();
                }
                return View(productTypeVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductTypeVM productTypeVM, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (productTypeVM.ProductType.Id == 0)
                {
                    productTypeVM.ProductType.DateAdded = DateTime.Now;
                    productTypeVM.ProductType.ModificationDate = DateTime.Now;
                    productTypeVM.ProductType.isActive = true;
                    _unitOfWork.ProductType.Add(productTypeVM.ProductType);
                }
                else
                {
                    productTypeVM.ProductType.ModificationDate = DateTime.Now;
                    _unitOfWork.ProductType.Update(productTypeVM.ProductType);
                }

                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null && files.Count > 0)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath, "images", "products", "product-" + productTypeVM.ProductType.Id);

                        if (!Directory.Exists(productPath))
                            Directory.CreateDirectory(productPath);

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProductImage productImage = new ProductImage
                        {
                            ImageUrl = Path.Combine("images", "products", "product-" + productTypeVM.ProductType.Id, fileName),
                            ProductTypeId = productTypeVM.ProductType.Id,
                        };

                        productTypeVM.ProductType.ProductImages ??= new List<ProductImage>();
                        productTypeVM.ProductType.ProductImages.Add(productImage);
                    }

                    _unitOfWork.ProductType.Update(productTypeVM.ProductType);
                    _unitOfWork.Save();
                }

                TempData["success"] = "Product Type saved successfully.";
                return RedirectToAction(nameof(Index));
            }

            productTypeVM.ManufacturerList = _unitOfWork.Manufacturer.GetAll()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
                .ToList();


            productTypeVM.ProductSubCategoryList = _unitOfWork.ProductSubCategory.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });

            return View(productTypeVM);
        }


        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.ProductImage.Get(u => u.Id == imageId);
            int productId = imageToBeDeleted.ProductTypeId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath =
                                   Path.Combine(_webHostEnvironment.WebRootPath,
                                   imageToBeDeleted.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.ProductImage.Remove(imageToBeDeleted);
                _unitOfWork.Save();

                TempData["success"] = "Deleted successfully";
            }

            return RedirectToAction(nameof(Upsert), new { id = productId });
        }

        // GET: ProductType/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _unitOfWork.ProductType.Get(
                p => p.Id == id,
                include: q => q
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductSubCategory));

            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productType = _unitOfWork.ProductType.Get(p => p.Id == id);
            if (productType != null)
            {
                productType.isActive = false;
                productType.DateOfDelete = DateTime.Now;
                _unitOfWork.ProductType.Update(productType);
                _unitOfWork.Save();
                TempData["success"] = "Product Type deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var productTypes = _unitOfWork.ProductType.GetAll(
             include: q => q
                 .Include(pt => pt.Manufacturer)
                 .Include(pt => pt.ProductSubCategory))
                    .Select(pc => new
                    {
                        pc.Id,
                        pc.Name,
                        pc.Descriptions,
                        pc.isActive,
                        pc.Price,
                        pc.ProductStatus,
                        Manufacturer = new
                        {
                            Name = pc.Manufacturer.Name
                        },
                        ProductSubCategory = new
                        {
                            Name = pc.ProductSubCategory.Name
                        }
                    })
                    .ToList();
                 return Json(new { data = productTypes });
        }

        #endregion

        private bool ProductTypeExists(int id)
        {
            return _unitOfWork.ProductType.Get(p => p.Id == id) != null;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddReview(int productTypeId, int rating, string comment)
        {
            if (productTypeId == 0)
            {
                return BadRequest("ProductTypeId is required and must be valid.");
            }

            var productType = _unitOfWork.ProductType.Get(pt => pt.Id == productTypeId);
            if (productType == null)
            {
                return NotFound("Product type not found.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = new Reviews
            {
                ProductTypeId = productTypeId,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                DateAdded = DateTime.Now,
                Name = $"Review by {userId} on {DateTime.Now}",
            };

            _unitOfWork.Reviews.Add(review);
            _unitOfWork.Save();

            return RedirectToAction("ShowProduct", "Home", new { id = productTypeId });
        }
    }
}
 

