using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class OrderStatusesRepository : Repository<OrderStatuses>, IOrderStatusesRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public OrderStatusesRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderStatuses obj)
        {
            _context.Update(obj);
        }
    }

}