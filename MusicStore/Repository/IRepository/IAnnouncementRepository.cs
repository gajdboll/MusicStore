using MusicStoreData.Models.CMS;
 

namespace MusicStore.Repository.IRepository
{
    public interface IAnnouncementRepository : IRepository<Announcement>
    {
        void Update(Announcement obj);

      }
}
