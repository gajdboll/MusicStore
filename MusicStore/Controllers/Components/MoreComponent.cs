using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{

    public class MoreComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public MoreComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MoreComponent", await _context.More.Where(p => p.isActive).ToListAsync());
        }
    }
}
