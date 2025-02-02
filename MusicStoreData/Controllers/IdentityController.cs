using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    public class IdentityController : Controller
    {
        public RoleManager<IdentityRole> RoleManger { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }

        public IdentityController(RoleManager<IdentityRole> roleManger, UserManager<IdentityUser> userManager)
        {
            RoleManger = roleManger;
            UserManager = userManager;
        }

 

        [HttpGet]
        [Route("/identity/add-user-to-role")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            IdentityUser? identityUser = await UserManager.FindByEmailAsync(email);
            if (!await RoleManger.RoleExistsAsync(roleName))
            {
                throw new ArgumentException($"Invalid role name: {roleName}");
            }
            if (identityUser != null)
            {
                if (!await UserManager.IsInRoleAsync(identityUser, roleName))
                {
                    await UserManager.AddToRoleAsync(identityUser, roleName);
                }
            }
            else
            {
                throw new ArgumentException($"Invalid user email: {email}");
            }
            return Ok();
        }
        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetUserRoles()
        {
             var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "User not found" });
            }
             var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User does not exist" });
            }
             var roles = await UserManager.GetRolesAsync(user);
             return Ok(roles);
        }
        [HttpGet]
        [Authorize]
        [Route("/identity/profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await UserManager.FindByIdAsync(userId);
            var userName = User.Identity.Name;


            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized" });
            }

            return Ok(new
            {
                user.UserName,
                user.Email,
                user.Id
            });
        }
    }
}
