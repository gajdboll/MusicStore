using MusicStoreData.Models.Store;
using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;

namespace MusicStore.Repository.IRepository
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        void Update(ProductCategory obj);
        bool IsPositionInUse(ProductCategory product);
        ProductCategory GetWithWebHeaders(int id);
        bool ProductCategoryExists(int id);

    }
}
