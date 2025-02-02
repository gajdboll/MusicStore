using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicUtilities;

namespace MusicStore.Controllers.Components
{

    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class RelatedProductsComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public RelatedProductsComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RelatedProductsComponent", await _context.ProductType.Where(p => p.isActive).ToListAsync());
        }
    }
}
