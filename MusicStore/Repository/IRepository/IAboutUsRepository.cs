using MusicStoreData.Models.CMS;
 
namespace MusicStore.Repository.IRepository
{
    public interface IAboutUsRepository : IRepository<AboutUs>
    {
        public void Update(AboutUs obj);
    }
}
