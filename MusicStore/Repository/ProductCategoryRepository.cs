using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;

namespace MusicStore.Repository
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ProductCategoryRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public bool IsPositionInUse(ProductCategory product)
        {
            return _context.ProductCategory.Any(m => m.Position == product.Position && m.Id != product.Id);

        }
 
        public ProductCategory GetWithWebHeaders(int id)
        {
            return _context.ProductCategory
                                    .Include(p => p.WebHeader)
                                    .FirstOrDefault(p => p.Id == id);
        }

        public bool ProductCategoryExists(int id)
        {
            return _context.WebHeaders.Any(e => e.Id == id);
        }
        public void Update(ProductCategory obj)
        {
            var objFromDb = _context.ProductCategory.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb !=null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                objFromDb.CategoryMoreInfo = obj.CategoryMoreInfo;
                objFromDb.WebHeaderId = obj.WebHeaderId;
                objFromDb.Position = obj.Position;
                if (obj.CategoryImageUrl != null)
                {
                    objFromDb.CategoryImageUrl = obj.CategoryImageUrl;
                }
            }

        }
    }
}
