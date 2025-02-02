using MusicStoreData.Models.CMS;
using MusicStoreData.Models.Store;

namespace MusicStore.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void Update(ProductImage obj);
     }
}
