using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.Basket;
using MusicStoreData.Models.ShoppingCart;
using System.Security.Claims;

namespace MusicStoreData.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly MusicStoreContext _context;
        public ShoppingController(MusicStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        [Route("api/addToCart")]
        public async Task<IActionResult> AddToBasket([FromBody] ShoppingCartRequest request)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new { message = "User not authorized" });
            }

            var productEntity = await _context.Product.FirstOrDefaultAsync(p => p.ProductTypeId == request.ProductTypeId && p.ColorId == request.ColorId);

            if (productEntity == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            var cartFromDb = await _context.ShoppingCarts
               .FirstOrDefaultAsync(u => u.ApplicationUserId == userId && u.ProductId == productEntity.Id);

            if (cartFromDb != null)
            {
                cartFromDb.Count += request.Count;
                _context.ShoppingCarts.Update(cartFromDb);
            }
            else
            {
                var newCartItem = new ShoppingCart()
                {
                    ApplicationUserId = userId,
                    ProductId = productEntity.Id,
                    Count = request.Count,
                    Price = request.Price
                };

                await _context.ShoppingCarts.AddAsync(newCartItem);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Cart updated successfully" });
        }

        [HttpGet]
        [Route("api/summaryCart")]
        public async Task<ActionResult<object>> GetShoppingCart()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var shoppingCart = await _context.ShoppingCarts
                                             .Where(u => u.ApplicationUserId == userId)
                                             .Include(sc => sc.Product)
                                             .ThenInclude(p => p.ProductType)
                                             .ThenInclude(p => p.ProductSubCategory)
                                             .Include(sc => sc.Product)
                                             .ThenInclude(p => p.Color)
                                             .ToListAsync();

            if (shoppingCart == null || !shoppingCart.Any())
            {
                return Ok(new { message = "Cart is empty", CartItems = new List<object>(), TotalCartPrice = 0 });
            }

            var cartItems = shoppingCart.Select(cart => new
            {
                ApplicationUserId = userId,
                ProductName = cart.Product.Name,
                ProductId = cart.ProductId,
                SuplierPrice = cart.Product.ProductType.SupplierPrice,
                ProductTypeName = cart.Product.ProductType.Name,
                Color = cart.Product.Color.Name,
                Quantity = cart.Count,
                Image = cart.Product.ProductType.ProductSubCategory.SubCategoryImageUrl,
                TotalProductPrice = cart.Count * cart.Product.ProductType.SupplierPrice
            }).ToList();

            var totalPrice = cartItems.Sum(item => item.TotalProductPrice);

            var cartResponse = new
            {
                CartItems = cartItems,
                TotalCartPrice = totalPrice
            };
            return Ok(cartResponse);
        }

        [HttpPut]
        [Route("api/shoppingcartitems")]
                public async Task<IActionResult> UpdateCartQuantity(int productId, string modification)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        return Unauthorized();
                    }

                    var cartItem = await _context.ShoppingCarts
                                                 .FirstOrDefaultAsync(c => c.ProductId == productId && c.ApplicationUserId == userId);

                    if (cartItem == null)
                    {
                        return NotFound(new { message = "Product not found in cart" });
                    }

                    if (modification == "increase")
                    {
                        cartItem.Count += 1;
                    }
                    else if (modification == "decrease" && cartItem.Count > 1)
                    {
                        cartItem.Count -= 1;
                    }
                    else if (modification == "decrease" && cartItem.Count == 1)
                    {
                        _context.ShoppingCarts.Remove(cartItem);
                    }
                    else if (modification == "delete")
                    {
                        _context.ShoppingCarts.Remove(cartItem);
                    }
                    else
                    {
                        return BadRequest(new { message = "Invalid action" });
                    }

                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Cart updated successfully", cartItem });
                }


    }

    public class ShoppingCartRequest
    {
        public int ProductTypeId { get; set; }
        public int ColorId { get; set; }
        public double TotalAmount { get; set; }
        public double Price { get; set; }
        public string ApplicationUserId { get; set; }
        public int Count { get; set; }
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }


    }

}
