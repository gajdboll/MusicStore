using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public WishlistRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Wishlist obj)
        {
            _context.Update(obj);
        }

 

    }
}
