using MusicStoreData.Models.CMS;


namespace MusicStore.Repository.IRepository
{
    public interface IWebHeaderRepository : IRepository<WebHeaders>
    {
        void Update(WebHeaders obj);
        bool IsPositionInUse(WebHeaders product);
        WebHeaders GetWithWebHeaders(int id);
        bool WebHeadersExists(int id);

    }
}
