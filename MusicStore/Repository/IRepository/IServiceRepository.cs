using MusicStoreData.Models.CMS;
 

namespace MusicStore.Repository.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service obj);

     }
}
