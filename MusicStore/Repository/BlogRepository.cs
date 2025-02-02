using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class BlogRepository : Repository<NewsAndArticles>, IBlogRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public BlogRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(NewsAndArticles obj)
        {
            _context.Update(obj);
        }

    }
}
