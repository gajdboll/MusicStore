using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;

namespace MusicStoreData.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly MusicStoreContext _context;
        public ProductController(MusicStoreContext context)
        {
            _context = context;
        }

        // 1 get all items and allows to list that on the main sceen - on first load
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories()
        {
            var categories = await _context.ProductCategory
               .Where(p => p.isActive)
               .Select(sc => new
               {
                   sc.Id,
                   sc.Name,
                   sc.Descriptions,
                   sc.CategoryImageUrl
               })
               .ToListAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound(new { message = "No categories found." });
            }

            return Ok(categories);
        }
        // 2 that endpoint is run when first category like Electric etc is selected - List of Categories returned from db
        [HttpGet("categories/{id}/subcategories")]
        public async Task<IActionResult> GetShowProductSubCategories(int id)
        {
            var subCategories = await _context.ProductSubCategory
                .Where(sc => sc.ProductCategoryId == id && sc.isActive)
                .Select(sc => new
                {
                    sc.Id,
                    sc.Name,
                    sc.Descriptions,
                    sc.SubCategoryImageUrl
                })
                .ToListAsync();

            return Ok(subCategories);
        }
        // 3 that endpoint is run when first category is selected from list of categories - return list of subcategories
        [HttpGet]
        [Route("subcategories/{id}/producttype")]
        public async Task<IActionResult> GetShowProductsFromSubCategory(int id)
        {
            var subCategory = await _context.ProductSubCategory
                .FirstOrDefaultAsync(p => p.Id == id && p.isActive);

            if (subCategory == null)
            {
                return NotFound(new { message = "Subcategory not found" });
            }

            var category = await _context.ProductCategory
               .FirstOrDefaultAsync(p => p.Id == subCategory.ProductCategoryId && p.isActive);

            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            var productsQuery = _context.ProductType
                .Include(p => p.ProductSubCategory)
                    .ThenInclude(psc => psc.ProductCategory)
                .Include(p => p.ProductImages)
                .Where(p => p.ProductSubCategoryId == id && p.isActive);

            var products = await productsQuery.ToListAsync();

            var manufacturers = await _context.Manufacturer
                .Where(p => p.isActive)
                .ToListAsync();

            var response = new
            {
                SubCategoryName = subCategory.Name,
                SubCategoryId = subCategory.Id,
                CategoryName = category.Name,
                CategoryId = category.Id,
                Manufacturers = manufacturers.Select(m => new
                {
                    m.Id,
                    m.Name
                }),
                Products = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.SupplierPrice,
                    Images = p.ProductImages.Select(img => img.ImageUrl).ToList(),
                    ManufacturerName = p.Manufacturer.Name
                }).ToList()
            };

            return Ok(response);
        }

        //4 that endpoint return single product type selected by specific id type from the list
        [HttpGet]
        [Route("producttype/{id}")]
        public async Task<IActionResult> GetShowProduct(int id)
        {
            var productType = await _context.ProductType
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSubCategory)
                    .ThenInclude(psc => psc.ProductCategory)
                .Include(p => p.Products)
                    .ThenInclude(pr => pr.Color)
                .FirstOrDefaultAsync(p => p.Id == id && p.isActive);

            if (productType == null)
            {
                return NotFound();
            }

            var productDetails = productType.Products.Select(pr => new
            {
                ProductId = pr.Id,
                ProductName = pr.Name,
                ProductColor = new
                {
                    Name = pr.Color.Name,
                    Id = pr.ColorId
                },
                ProductQuantity = pr.Quantity
            }).ToList();

            var response = new
            {

                ProductTypeId = productType.Id,
                ProductTypeName = productType.Name,
                ProductDescriptions = productType.Descriptions,
                ProductOffer = productType.OnSale,
                ProductTypePrice = productType.Price,
                ProductSupplierPrice = productType.SupplierPrice,
                ManufacturerName = productType.Manufacturer?.Name,
                ProductDetails = productDetails,
                ProductImages = productType.ProductImages.Select(img => img.ImageUrl).ToList()
            };

            return Ok(response);
        }

        [HttpGet("api/product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.ProductType
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSubCategory)
                    .ThenInclude(psc => psc.ProductCategory)
                .Include(p => p.Products)
                    .ThenInclude(pr => pr.Color)
                .FirstOrDefaultAsync(p => p.Id == id && p.isActive);

            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            return Ok(new
            {
                ProductTypeId = product.Id,
                Name = product.Name,
                Descriptions = product.Descriptions,
                Price = product.Price,
                SupplierPrice = product.SupplierPrice,
                OnSale = product.OnSale,
                Manufacturer = product.Manufacturer.Name,
                Images = product.ProductImages.Select(img => img.ImageUrl).ToList(),
                SubCategory = new
                {
                    Id = product.ProductSubCategoryId,
                    Name = product.ProductSubCategory.Name,
                    Category = new
                    {
                        Id = product.ProductSubCategory.ProductCategory.Id,
                        Name = product.ProductSubCategory.ProductCategory.Name
                    }
                },
                RelatedProducts = product.Products.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Color = p.Color.Name
                }).ToList()
            });
        }

        [HttpGet]
        [Route("api/product/lowlevel")]
        public async Task<IActionResult> GetLowProducts()
        {
            var lowProducts = await _context.Product
                .Include(prod => prod.Color)
                .Include(prod => prod.ProductType)
                .ThenInclude(pt => pt.ProductImages)
                .Where(prod => prod.Quantity <= 2 && prod.isActive)
                .Select(prod => new
                {
                    Id = prod.Id,
                    ProductTypeId = prod.ProductTypeId,
                    ColorId = prod.ColorId,
                    ColorName = prod.Color.Name,
                    Price = prod.ProductType.Price,
                    SupplierPrice = prod.ProductType.SupplierPrice,
                    Quantity = prod.Quantity,
                    ImageUrl = prod.ProductType.ProductImages.Any() ? prod.ProductType.ProductImages.First().ImageUrl : "default-image-url.png",
                    Name = prod.ProductType.Name
                })
                .ToListAsync();

            if (lowProducts == null || !lowProducts.Any() || lowProducts.Count == 0)
            {
                return Ok(new { message = "No low products found." });
            }

            return Ok(lowProducts);
        }
        [HttpGet("api/product/bycolor/{colorId}")]
        public async Task<IActionResult> GetProductByColor(int colorId)
        {
            var product = await _context.Product
                .Include(p => p.Color)
                .FirstOrDefaultAsync(p => p.Color.Id == colorId && p.isActive);

            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }
    }
}