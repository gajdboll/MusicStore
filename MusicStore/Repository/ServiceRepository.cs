using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ServiceRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Service obj)
        {
            _context.Update(obj);
        }
    }

}