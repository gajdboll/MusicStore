
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.Users;

namespace MusicStore.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public CompanyRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Company obj)
        {
            _context.Update(obj);
        }

        public bool IsPositionInUse(Company company)
        {
            return _context.Company.Any(m => m.Position == company.Position && m.Id != company.Id);

        }

        public Company GetWithCompany(int id)
            {
                return _context.Company.FirstOrDefault(p => p.Id == id);

            }
    }
}
