using MusicStoreData.Models.CMS;

namespace MusicStore.Repository.IRepository
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        void Update(Manufacturer obj);
        bool IsPositionInUse(Manufacturer product);
        Manufacturer GetWithManufacturer(int id);
        bool ManufacturerExists(int id);        
    }
}
 
