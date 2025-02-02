using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository
{
    public class WishlistItemRepository : Repository<WishlistItem>, IWishlistItemsRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public WishlistItemRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(WishlistItem obj)
        {
            _context.Update(obj);
        }

 

    }
}
