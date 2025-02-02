using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{

    public class FAQComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public FAQComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int productTypeId)
        {
            var faqs = await _context.FAQ
            .Where(r => r.ProductTypeId == productTypeId)
            .ToListAsync();
            return View("FAQComponent", faqs);
        }

    }
}