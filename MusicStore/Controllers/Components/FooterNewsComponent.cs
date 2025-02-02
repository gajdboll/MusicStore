using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{


    public class FooterNewsComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public FooterNewsComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("FooterNewsComponent", await _context.NewsAndArticles.Where(p => p.isActive).OrderBy(x => Guid.NewGuid()).Take(3).ToListAsync());
        }
    }
}
