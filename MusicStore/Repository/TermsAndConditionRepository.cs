
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;

namespace MusicStore.Repository
{
    public class TermsAndConditionRepository : Repository<TermsAndCondition>, ITermsAndConditionRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public TermsAndConditionRepository(MusicStoreContext context) : base(context) 
        {
            _context = context;
         }

        public void Update(TermsAndCondition obj)
        {
             var objFromDb = _context.TermsAndCondition.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.TermsAndConditionMoreInfo = obj.TermsAndConditionMoreInfo;
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                 objFromDb.Position = obj.Position;
                if (obj.TermsPhotoUrl!= null)
                {
                    objFromDb.TermsPhotoUrl = obj.TermsPhotoUrl;
                }
            }

        }
  
        public bool IsPositionInUse(TermsAndCondition product) 
        {
         return _context.TermsAndCondition.Any(m => m.Position == product.Position && m.Id != product.Id);

        }
 
 
    }
}
