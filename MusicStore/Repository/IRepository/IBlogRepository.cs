using MusicStoreData.Models.CMS;
 

namespace MusicStore.Repository.IRepository
{
    public interface IBlogRepository : IRepository<NewsAndArticles>
    {
        void Update(NewsAndArticles obj);
     }
}
