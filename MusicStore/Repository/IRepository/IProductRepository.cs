using MusicStoreData.Models.Store;
using MusicStoreData.Models.Store;

namespace MusicStore.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        bool IsPositionInUse(Product product);
        Product GetWithColor(int id);
        bool ProductExists(int id);
    }
}
 