using MusicStoreData.Models.Store;
 
namespace MusicStore.Repository.IRepository
{
    public interface IProductSubCategoryRepository : IRepository<ProductSubCategory>
    {
        void Update(ProductSubCategory entity);
        ProductSubCategory GetWithCategory(int id);
        bool ProductSubCategoryExists(int id);
        bool IsPositionInUse(ProductSubCategory product);
    }
}
