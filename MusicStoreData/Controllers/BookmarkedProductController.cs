using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;
using Newtonsoft.Json;
using System.Security.Claims;
 
namespace MusicStoreData.Controllers
{
    public class BookmarkedProductController : Controller
    {
        private readonly MusicStoreContext _context;

        public BookmarkedProductController(MusicStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/bookmarks/add")]
        public async Task<IActionResult> AddBookmark([FromBody] BookmarkedProductRequest request)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var bookmark = new BookmarkedProduct
            {
                ApplicationUserId = userId,
                ProductId = request.ProductId,
                Name = request.Name,
                Detail = request.Detail,
             
                ImageUrl = request.ImageUrl,
                IsBookmarked = true
            };

            _context.BookmarkedProduct.Add(bookmark);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bookmark added successfully." });
        }

        [HttpDelete]
        [Route("api/bookmarks/remove/{productId}")]
        public async Task<IActionResult> RemoveBookmark (int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var bookmark = await _context.BookmarkedProduct
                .FirstOrDefaultAsync(b => b.ProductId == productId && b.ApplicationUserId == userId);

            if (bookmark == null)
            {
                return NotFound(new { message = "Bookmark not found." });
            }

            _context.BookmarkedProduct.Remove(bookmark);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bookmark removed successfully." });
        }

        [HttpGet]
        [Route("api/bookmarks/isbookmarked/{productId}")]
        public async Task<IActionResult> IsBookmarked(int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var isBookmarked = await _context.BookmarkedProduct
                .AnyAsync(b => b.ProductId == productId && b.ApplicationUserId == userId);

            return Ok(new { isBookmarked });
        }
        [HttpGet]
        [Route("api/bookmarks/list")]
        public async Task<IActionResult> GetBookmarks()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var bookmarks = await _context.BookmarkedProduct
                .Where(b => b.ApplicationUserId == userId)
                .ToListAsync();

            return Ok(bookmarks);
        }

    }
    public class BookmarkedProductRequest
    {
 
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
      }
}
