using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public AnnouncementRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Concert obj)
        {
            _context.Update(obj);
        }

    }
}
