using MusicStoreData.Models.CMS;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository.IRepository
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        void Update(Wishlist obj);
     }
}
