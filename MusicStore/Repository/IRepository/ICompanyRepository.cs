using MusicStoreData.Models.Users;

namespace MusicStore.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);

        bool IsPositionInUse(Company company);
        Company GetWithCompany(int id);
    }
}
