using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicUtilities;

namespace MusicStore.Controllers
{

    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ControlActionController : Controller
    {
        private readonly MusicStoreContext _context;

        public ControlActionController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: ControlAction 
        public IActionResult Index()
        {
            var tableNames = _context.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.Name)
                .OrderBy(p => p)
                .ToList();

            return View(tableNames);
        }
  
    }
}
