using MusicStoreData.Models.CMS;
 

namespace MusicStore.Repository.IRepository
{
    public interface IConcertRepository : IRepository<Concert>
    {
        void Update(Concert obj);

        bool IsPositionInUse(Concert concert);
     }
}
