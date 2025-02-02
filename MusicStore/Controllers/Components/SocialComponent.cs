using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicUtilities;

namespace MusicStore.Controllers.Components
{

    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class SocialComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public SocialComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SocialComponent", await _context.SocialMedia.Where(p => p.isActive).ToListAsync());
        }
    }
}
