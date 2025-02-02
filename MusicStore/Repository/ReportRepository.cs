using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private MusicStoreContext _context;
          public ReportRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Report obj)
        {
            _context.Update(obj);
        }
    }
}