using MusicStoreData.Models.CMS;

namespace MusicStore.Repository.IRepository
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        void Update(Gallery obj);
     }
}
