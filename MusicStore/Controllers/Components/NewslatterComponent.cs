using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;

namespace MusicStore.Controllers.Components
{


    public class NewslatterComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public NewslatterComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View("NewslatterComponent", await _context.Newslatter.FindAsync(id));
        }
    }
}
