using MusicStoreData.Models.CMS;
using MusicStoreData.Models.Store;
using MusicStoreData.Models.Users;

namespace MusicStore.Repository.IRepository
{
    public interface ITermsAndConditionRepository : IRepository<TermsAndCondition>
    {
        void Update(TermsAndCondition obj);

        bool IsPositionInUse(TermsAndCondition company);
     }
}
