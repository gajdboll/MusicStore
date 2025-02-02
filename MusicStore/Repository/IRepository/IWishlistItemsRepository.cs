 using MusicStoreData.Models.ShoppingCart;
 

namespace MusicStore.Repository.IRepository
{
    public interface IWishlistItemsRepository : IRepository<WishlistItem>
    {
        void Update(WishlistItem obj);

    }
}
