using MusicStoreData.Models.CMS;
 

namespace MusicStore.Repository.IRepository
{
    public interface IDiscountCodeRepository : IRepository<DiscountCode>
    {
        void Update(DiscountCode obj);

      }
}
