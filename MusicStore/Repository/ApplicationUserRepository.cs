
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.Users;

namespace MusicStore.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private MusicStoreContext _context;
          public ApplicationUserRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }
        public void Update(ApplicationUser applicationUser)
        {
            _context.ApplicationUsers.Update(applicationUser);
        }
    }
}