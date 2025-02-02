using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.IRepository;
using MusicStoreData.Models.ShoppingCart;
using MusicUtilities;
using System.Security.Claims;

[Authorize]
public class WishlistController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public WishlistController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var wishlist = _unitOfWork.Wishlist.GetAll(
            w => w.ApplicationUserId == userId,
            include: w => w.Include(w => w.WishlistItems)
                           .ThenInclude(wi => wi.ProductType)
                               .ThenInclude(pt => pt.Manufacturer)
                           .Include(w => w.WishlistItems)
                               .ThenInclude(wi => wi.ProductType)
                                   .ThenInclude(pt => pt.ProductSubCategory)
                                       .ThenInclude(psc => psc.ProductCategory)
                           .Include(w => w.WishlistItems)
                               .ThenInclude(wi => wi.ProductType)
                                   .ThenInclude(pt => pt.ProductImages)
        ).FirstOrDefault();

        return View(wishlist);
    }

    public IActionResult AddToWishlist(int productTypeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var wishlist = _unitOfWork.Wishlist.Get(w => w.ApplicationUserId == userId);

        if (wishlist == null)
        {
            wishlist = CreateNewWishlist(userId);
        }

        AddProductToWishlist(wishlist, productTypeId);

         HttpContext.Session.SetInt32(StaticDetails.SessionWishlist, wishlist.WishlistItems.Count);

        return RedirectToAction(nameof(Index));
    }

    private Wishlist CreateNewWishlist(string userId)
    {
        var newWishlist = new Wishlist
        {
            ApplicationUserId = userId,
            WishlistItems = new List<WishlistItem>()
        };
        _unitOfWork.Wishlist.Add(newWishlist);
        _unitOfWork.Save();
        return newWishlist;
    }

    private void AddProductToWishlist(Wishlist wishlist, int productTypeId)
    {
         if (wishlist.WishlistItems.All(wi => wi.ProductTypeId != productTypeId))
        {
            var newWishlistItem = new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductTypeId = productTypeId
            };

            _unitOfWork.WishlistItems.Add(newWishlistItem);  
            _unitOfWork.Save();  
        }
    }

    public IActionResult RemoveFromWishlist(int productTypeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

         var wishlistItem = _unitOfWork.WishlistItems.Get(
            wi => wi.Wishlist.ApplicationUserId == userId && wi.ProductTypeId == productTypeId
        );

        if (wishlistItem != null)
        {
            _unitOfWork.WishlistItems.Remove(wishlistItem);
            _unitOfWork.Save();

             var wishlist = _unitOfWork.Wishlist.Get(w => w.ApplicationUserId == userId);
            HttpContext.Session.SetInt32(StaticDetails.SessionWishlist, wishlist?.WishlistItems.Count ?? 0);
        }

        return RedirectToAction(nameof(Index));
    }
}
