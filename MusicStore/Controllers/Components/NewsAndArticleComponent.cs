
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;


namespace MusicStore.Controllers.Components
{


    public class NewsAndArticleComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public NewsAndArticleComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("NewsAndArticleComponent", await _context.NewsAndArticles.Where(p => p.isActive).ToListAsync());
        }
    }
}