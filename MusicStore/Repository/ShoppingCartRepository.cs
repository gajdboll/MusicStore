
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Store;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.Users;
using MusicStoreData.Models.Basket;

namespace MusicStore.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ShoppingCartRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ShoppingCart obj)
        {
            _context.Update(obj);
        }

  
    }
}
