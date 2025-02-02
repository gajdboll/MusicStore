using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;

namespace MusicStore.Repository
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public GalleryRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Gallery obj)
        {
            _context.Update(obj);
        }

 

    }
}
