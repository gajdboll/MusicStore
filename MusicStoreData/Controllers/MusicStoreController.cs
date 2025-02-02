using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;

namespace MusicStoreData.Controllers
{
    [ApiController]
    public class StoreAddressController : ControllerBase
    {
        private readonly MusicStoreContext _context;

        public StoreAddressController(MusicStoreContext context)
        {
            _context = context;
        }

         [HttpGet]
        [Route("storeAddress")]
        public IActionResult GetStoreAddress()
        {
            var storeAddress = _context.MusicStoreAddress.FirstOrDefault(a => a.isActive && a.IsItCurrentStore);

            if (storeAddress == null)
            {
                return NotFound(new { message = "Store address not found" });
            }

            return Ok(storeAddress);
        }
    
        [HttpGet]
        [Route("openingHours")]
        public IActionResult GetOpeningHours()
        {
            var openingHours = _context.OpeningHours
                                       .Where(h => h.MusicStoreAddressId == 1) 
                                       .OrderBy(h => h.Id)
                                       .ToList();

            if (!openingHours.Any())
            {
                return NotFound(new { message = "Opening hours not found" });
            }

            return Ok(openingHours);
        }
    }
}