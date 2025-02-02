using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{

    public class StoreAddressComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public StoreAddressComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var address = await _context.MusicStoreAddress
                .Where(p => p.isActive)
                .Include(pc => pc.OpeningHours)
                .ToListAsync();

            return View("StoreAddressComponent", address);
        }

    }
}
