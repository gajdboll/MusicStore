
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;

namespace MusicStore.Repository
{
    public class MoreRepository : Repository<More>, IMoreRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public MoreRepository(MusicStoreContext context) : base(context) 
        {
            _context = context;
         }

        public void Update(More obj)
        {
             var objFromDb = _context.More.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                 objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                 objFromDb.Position = obj.Position;
                if (obj.MoreIcon!= null)
                {
                    objFromDb.MoreIcon = obj.MoreIcon;
                }
            }

        }
  
        public bool IsPositionInUse(More product) 
        {
         return _context.More.Any(m => m.Position == product.Position && m.Id != product.Id);

        }
 
 
    }
}
