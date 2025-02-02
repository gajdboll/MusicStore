using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;

namespace MusicStore.Repository
{
    public class WebHeaderRepository : Repository<WebHeaders>, IWebHeaderRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public WebHeaderRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public bool IsPositionInUse(WebHeaders product)
        {
            return _context.WebHeaders.Any(m => m.Position == product.Position && m.Id != product.Id);

        }
   
        public WebHeaders GetWithWebHeaders(int id)
        {
            return _context.WebHeaders.FirstOrDefault(p => p.Id == id);
        }

        public bool WebHeadersExists(int id)
        {
            return _context.WebHeaders.Any(e => e.Id == id);
        }
        public void Update(WebHeaders obj)
        {
            var objFromDb = _context.WebHeaders.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                objFromDb.Position = obj.Position;
                objFromDb.isActive = obj.isActive;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
