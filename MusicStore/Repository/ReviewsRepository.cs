using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class ReviewsRepository : Repository<Reviews>, IReviewsRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ReviewsRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Reviews obj)
        {
            _context.Update(obj);
        }
    }

}