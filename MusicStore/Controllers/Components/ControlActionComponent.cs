using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicUtilities;

namespace MusicStore.Controllers.Components
{

    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ControlActionComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public ControlActionComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("ControlActionComponent", await _context.ControlActions.Where(p => p.isActive).ToListAsync());
        }
    }
}
