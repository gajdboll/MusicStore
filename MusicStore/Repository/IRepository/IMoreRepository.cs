using MusicStoreData.Models.CMS;
using MusicStoreData.Models.Store;
using MusicStoreData.Models.Users;

namespace MusicStore.Repository.IRepository
{
    public interface IMoreRepository : IRepository<More>
    {
        void Update(More obj);

        bool IsPositionInUse(More more);
     }
}
