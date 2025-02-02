using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{
    public class ReviewsComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public ReviewsComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int productTypeId)
        {
            var reviews = await _context.Reviews
         .Where(r => r.ProductTypeId == productTypeId && r.isActive)
         .Include(r => r.User)
         .ToListAsync();

            return View("ReviewsComponent", reviews);
        }
    }
}
