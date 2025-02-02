using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{

    public class TermsAndConditionNavigationComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public TermsAndConditionNavigationComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("TermsAndConditionNavigationComponent", await _context.TermsAndCondition.Where(p => p.isActive).ToListAsync());
        }
    }
}
