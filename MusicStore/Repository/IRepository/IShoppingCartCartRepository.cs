using MusicStoreData.Models.Basket;
using MusicStoreData.Models.Users;

namespace MusicStore.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);

    }
}
