
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.CMS;

namespace MusicStore.Repository
{
    public class AboutUsRepository : Repository<AboutUs>, IAboutUsRepository
    {
        private MusicStoreContext _context;
          public AboutUsRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }
        public void Update(AboutUs obj)
        {
            _context.Update(obj);
        }
    }
}