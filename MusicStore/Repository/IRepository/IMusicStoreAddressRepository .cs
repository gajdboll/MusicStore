using MusicStoreData.Models.CMS;
 
namespace MusicStore.Repository.IRepository
{
    public interface IMusicStoreAddressRepository : IRepository<MusicStoreAddress>
    {
        void Update(MusicStoreAddress obj);
 
    }
}
