using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{
    public class FooterInfoLinksComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public FooterInfoLinksComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("FooterInfoLinksComponent", await _context.More.Where(p => p.isActive).ToListAsync());
        }
    }
}
