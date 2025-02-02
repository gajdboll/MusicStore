 using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository.IRepository
{
    public interface IReportRepository : IRepository<Report>
    {
        public void Update(Report obj);
    }
}
