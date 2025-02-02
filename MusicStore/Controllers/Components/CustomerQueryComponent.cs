using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers.Components
{

    public class CustomerQueryComponent : ViewComponent
    {
        private readonly MusicStoreContext _context;
        public CustomerQueryComponent(MusicStoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("CustomerQueryComponent", new CustomerQuery());
        }
    }
}
