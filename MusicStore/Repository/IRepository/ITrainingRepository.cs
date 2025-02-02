using MusicStoreData.Models.CMS;
 

namespace MusicStore.Repository.IRepository
{
    public interface ITrainingRepository : IRepository<Training>
    {
        void Update(Training obj);

      }
}
