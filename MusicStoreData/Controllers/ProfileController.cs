using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;


namespace MusicStoreData.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MusicStoreContext _context;
        public ProfileController(MusicStoreContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("profile/policies")]
        public async Task<ActionResult<List<AdminPolicies>>> GetAdminPolicies()
        {
            var policies = await _context.AdminPolicies
                                         .Where(p => p.isActive)
                                         .Select(p => new AdminPolicies
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Descriptions = p.Descriptions,
                                         })
                                         .ToListAsync();

            return Ok(policies);
        }
        [HttpGet]
        [Route("profile/musicaddress")]
        public async Task<ActionResult<MusicStoreAddress>> GetMusicAddress()
        {
            var address = await _context.MusicStoreAddress
                                         .Where(p => p.isActive & p.IsItCurrentStore == true)
                                         .Select(p => new MusicStoreAddress
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Descriptions = p.Descriptions,
                                             Address = p.Address,
                                             Address2 = p.Address2,
                                             PhoneNumber = p.PhoneNumber,
                                             EmailAdres = p.EmailAdres,
                                             Country = p.Country,
                                             PostCode = p.PostCode
                                         })
                                         .FirstOrDefaultAsync();

            return Ok(address);
        }
    }
}
