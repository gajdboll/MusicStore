using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;


namespace MusicStore.Controllers.Components
{
    public class MessagesComponent : ViewComponent
    {

        private readonly MusicStoreContext _context;
        public MessagesComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = _context.CustomerQuery.Where(p => p.isActive && !p.IsAnswered).Count();


            return View(query);

        }
    }
}
