using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicUtilities;

namespace MusicStore.Controllers.Components
{
    [Authorize(Roles = $"{StaticDetails.Role_Customer},{StaticDetails.Role_Employee},{StaticDetails.Role_Company}")]
    public class SubscriptionComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public SubscriptionComponent(MusicStoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var subscription = await _context.Subscription
                                            .Where(p => p.isActive)
                                            .FirstOrDefaultAsync();

            return View("SubscriptionComponent", subscription);
        }

    }
}
