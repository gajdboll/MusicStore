
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
 
namespace MusicStore.Repository
{
    public class DiscountCodeRepository : Repository<DiscountCode>, IDiscountCodeRepository 
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public DiscountCodeRepository(MusicStoreContext context) : base(context) 
        {
            _context = context;
         }
 
  
        public bool IsPositionInUse(DiscountCode product) 
        {
            return _context.DiscountCode.Any(m => m.Position == product.Position && m.Id != product.Id);

        }

  
    }
}
