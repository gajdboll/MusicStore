using MusicStoreData.Models.Store;

namespace MusicStore.Repository.IRepository
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        void Update(ProductType obj);

        bool IsPositionInUse(ProductType company);
     }
}
