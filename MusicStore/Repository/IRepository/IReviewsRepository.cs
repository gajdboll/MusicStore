  

namespace MusicStore.Repository.IRepository
{
    public interface IReviewsRepository : IRepository<Reviews>
    {
        void Update(Reviews obj);

      }
}
