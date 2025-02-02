using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicUtilities;

namespace MusicStore.Controllers.Components
{

    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class AdminManagmentComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public AdminManagmentComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminManagmentComponent", await _context.ControlActions.Where(p => p.isActive).ToListAsync());
        }
    }
}
