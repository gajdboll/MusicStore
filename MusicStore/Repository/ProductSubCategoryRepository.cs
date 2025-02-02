
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;

namespace MusicStore.Repository
{
    public class ProductSubCategoryRepository : Repository<ProductSubCategory>, IProductSubCategoryRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ProductSubCategoryRepository(MusicStoreContext context) : base(context) 
        {
            _context = context;
         }

        public void Update(ProductSubCategory obj)
        {
             var objFromDb = _context.ProductSubCategory.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.ProductCategoryId = obj.ProductCategoryId;
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                objFromDb.SubCategoryMoreInfo = obj.SubCategoryMoreInfo;
                objFromDb.Position = obj.Position;
                objFromDb.isActive = obj.isActive;
                if (obj.SubCategoryImageUrl!= null)
                {
                    objFromDb.SubCategoryImageUrl = obj.SubCategoryImageUrl;
                }
            }

        }
  
        public bool IsPositionInUse(ProductSubCategory product) 
        {
         return _context.ProductSubCategory.Any(m => m.Position == product.Position && m.Id != product.Id);
        }

        public bool ProductSubCategoryExists(int id)
        {
            var currentSubCategory = _context.ProductSubCategory.Find(id);
            if (currentSubCategory == null)
            {
 
                return false;
            }
            return !(_context.ProductSubCategory
                           .Any(e => e.Position == currentSubCategory.Position && e.Id != id));

        }

        public ProductSubCategory GetWithCategory(int id)
        {
            return _context.ProductSubCategory
                                    .Include(p => p.ProductCategory)
                                    .FirstOrDefault(p => p.Id == id);
        }
    }
}
