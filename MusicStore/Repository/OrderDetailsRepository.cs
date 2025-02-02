
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.Users;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public OrderDetailsRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetails obj)
        {
            _context.Update(obj);
        }
    }
}
