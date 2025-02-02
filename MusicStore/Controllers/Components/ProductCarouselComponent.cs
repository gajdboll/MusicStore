using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;

namespace MusicStore.Controllers.Components
{
    public class ProductCarouselComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;

        public ProductCarouselComponent(MusicStoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string filterType)
        {
            IQueryable<ProductType> productsQuery = _context.ProductType
                                         .Include(pt => pt.Manufacturer)
                                            .Include(pt => pt.ProductSubCategory);

            switch (filterType)
            {
                case "new":
                    productsQuery = productsQuery.Where(p => p.ProductStatus);  // check is product new
                    break;
                case "sale":
                    productsQuery = productsQuery.Where(p => p.OnSale);  // check is product on sale
                    break;
                case "old":
                    productsQuery = productsQuery.Where(p => !p.ProductStatus);  // check is product Already used and older
                    break;
                default:
                    productsQuery = productsQuery.Where(p => p.ProductStatus);   // default value
                    break;
            }

            var products = await productsQuery.ToListAsync();

            return View(products);
        }
    }
}
