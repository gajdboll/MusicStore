using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
 
namespace MusicStore.Repository
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        public readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ProductTypeRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }
 
        public bool IsPositionInUse(ProductType product)
        {
            return _context.ProductType.Any(m => m.Position == product.Position && m.Id != product.Id);

        }

        public void Update(ProductType obj)
        {
            var objFromDb = _context.ProductType.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb !=null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                objFromDb.Price = obj.Price;
                objFromDb.ProductStatus = obj.ProductStatus;
                objFromDb.Position = obj.Position;
                objFromDb.ManufacturerId = obj.ManufacturerId;
                objFromDb.ProductSubCategoryId = obj.ProductSubCategoryId;
                objFromDb.MoreInfo = obj.MoreInfo;
                objFromDb.isActive = obj.isActive;
                
            }

        }
    }
}
