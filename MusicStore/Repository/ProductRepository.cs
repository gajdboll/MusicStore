using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
 

namespace MusicStore.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ProductRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public Product GetWithColor(int id)
        {
            return _context.Product.Include(p => p.Color)
                                               .FirstOrDefault(p => p.Id == id);
        }

        public bool IsPositionInUse(Product product)
        {
            return _context.Product.Any(m => m.Position == product.Position && m.Id != product.Id);

        }

        public bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public void Update(Product obj)
        {
            var objFromDb = _context.Product.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb !=null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                objFromDb.ProductTypeId = obj.ProductTypeId;
                objFromDb.ColorId = obj.ColorId;
                objFromDb.isActive = obj.isActive;
                objFromDb.Position = obj.Position;
                objFromDb.Quantity = obj.Quantity;
 
                
            }

        }
 
    }
}
