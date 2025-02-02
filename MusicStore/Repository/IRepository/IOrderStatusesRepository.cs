using MusicStoreData.Models.CMS;
 
namespace MusicStore.Repository.IRepository
{
    public interface IOrderStatusesRepository : IRepository<OrderStatuses>
    {
        public void Update(OrderStatuses obj);
    }
}
