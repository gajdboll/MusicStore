
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;

namespace MusicStore.Repository
{
    public class ConcertRepository : Repository<Concert>, IConcertRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ConcertRepository(MusicStoreContext context) : base(context) 
        {
            _context = context;
         }
 
  
        public bool IsPositionInUse(Concert product) 
        {
            return _context.Concert.Any(m => m.Position == product.Position && m.Id != product.Id);

        }

  
    }
}
