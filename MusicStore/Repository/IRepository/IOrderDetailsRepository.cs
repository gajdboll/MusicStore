using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
     }
}
