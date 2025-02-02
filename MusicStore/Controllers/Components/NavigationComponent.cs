using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{


    public class NavigationComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public NavigationComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("NavigationComponent", await _context.WebHeaders.Where(p => p.isActive)
                .Include(u => u.ProductCategories)
                .ThenInclude(s => s.ProductSubCategories)
                .OrderBy(o => o.Position).ToListAsync());
        }
    }
}
