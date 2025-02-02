using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.BusinesLogic;
using System.Security.Claims;

namespace MusicStoreData.Controllers
{
    public class OrderController : Controller
    {
        private readonly MusicStoreContext _context;
        public OrderController(MusicStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest orderRequest)
        {
             var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

             foreach (var item in orderRequest.Items)
            {
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Quantity += item.Quantity; 
                }
            }

             var newOrder = new Order
            {
                ApplicationUserId = userId,
                Name = $"Order-{DateTime.Now:yyyyMMddHHmmss}",
                OrderDate = DateTime.UtcNow,
                TotalAmount = orderRequest.TotalAmount,  
                Address = orderRequest.Address,
                OrderItems = orderRequest.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,  
                    Quantity = item.Quantity,
                    ProductSuplierPrice = item.SuplierPrice,
                }).ToList()  
            };

             _context.Order.Add(newOrder);

             await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully, stock levels updated, and order saved." });
        }

        [HttpGet]
        [Route("api/getOrders/{userId}")]
        public async Task<IActionResult> GetOrders(string userId)
        {
             if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "UserId is required." });
            }

             var orders = await _context.Order
                                       .Where(o => o.ApplicationUserId == userId)  
                                       .Include(o => o.OrderItems)  
                                       .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found for this user." });
            }

            return Ok(orders);  
        }
        [HttpGet]
        [Route("api/getOrderItems/{orderId}")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
             var order = await _context.Order
                                      .Include(o => o.OrderItems)
                                      .ThenInclude(oi => oi.Product)  
                                      .ThenInclude(oi => oi.ProductType)  
                                      .ThenInclude(oi => oi.ProductImages)  
                                      .FirstOrDefaultAsync(o => o.Id == orderId);

             if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            var orderItems = order.OrderItems.Select(item => new
            {
               ProductId = item.ProductId,
               Quantity = item.Quantity,
               SuplierPrice = item.ProductSuplierPrice,
   
               ProductName = item.Product.ProductType.Name,
               ImageURL = item.Product.ProductType.ProductImages.FirstOrDefault().ImageUrl  

            }).ToList();


            return Ok(orderItems);  
        }

        public class OrderRequest
        {
            public List<OrderItemRequest> Items { get; set; }
            public string Address { get; set; }
            public double TotalAmount { get; set; }
            public string ApplicationUserId { get; set; }
            public DateTime OrderDate { get; set; }
            public string Name { get; set; }
            public int Id { get; set; }

        }

        public class OrderItemRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public double SuplierPrice { get; set; }
        }

    }
}
