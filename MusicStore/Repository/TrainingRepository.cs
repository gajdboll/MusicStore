using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public TrainingRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }
 
        public void Update(Training obj)
        {
            _context.Update(obj);
        }
 
    }
}
