using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class MusicStoreAddressRepository : Repository<MusicStoreAddress>, IMusicStoreAddressRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public MusicStoreAddressRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(MusicStoreAddress obj)
        {
            _context.Update(obj);
        }
    }

}