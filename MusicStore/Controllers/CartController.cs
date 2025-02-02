using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.IRepository;
using MusicStore.ViewModels;
using MusicStoreData.Models.Basket;
using MusicStoreData.Models.ShoppingCart;
using MusicStoreData.Models.Users;
using MusicUtilities;
using Stripe.Checkout;
using System.Security.Claims;

public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    [BindProperty]
    public ShoppingCartVM ShoppingCartVM { get; set; }

    public CartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new()
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                u => u.ApplicationUserId == userId,
                include: q => q.Include(sc => sc.Product)
                                .ThenInclude(p => p.ProductType)
                                .ThenInclude(p => p.ProductSubCategory)
                                .ThenInclude(p => p.ProductCategory)
                              .Include(sc => sc.Product)
                                .ThenInclude(p => p.ProductType)
                                .ThenInclude(p => p.Manufacturer)),
            OrderHeader = new()
        };

        foreach (var cart in ShoppingCartVM.ShoppingCartList)
        {
            var basePrice = cart.Product.ProductType.OnSale ? cart.Product.ProductType.Price * 0.5m : cart.Product.ProductType.Price;
            cart.Price = GetPriceBasedOnQuantity(cart, basePrice);
            ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }
        return View(ShoppingCartVM);
    }


    public IActionResult Plus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
        cartFromDb.Count += 1;
        _unitOfWork.ShoppingCart.Update(cartFromDb);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Minus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
        if (cartFromDb.Count <= 1)
        {
            HttpContext.Session.SetInt32(StaticDetails.SessionCart,
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
        }
        else
        {
            cartFromDb.Count -= 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
        HttpContext.Session.SetInt32(StaticDetails.SessionCart,
            _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
        _unitOfWork.ShoppingCart.Remove(cartFromDb);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Summary()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new()
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                include: q => q.Include(sc => sc.Product)
                               .ThenInclude(p => p.ProductType)
                               .ThenInclude(p => p.ProductSubCategory)
                               .ThenInclude(p => p.ProductCategory)
                              .Include(sc => sc.Product)
                               .ThenInclude(p => p.ProductType)
                               .ThenInclude(p => p.Manufacturer)),
            OrderHeader = new()
        };

        ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
        ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
        ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

        ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.FirstName + " " +
                                          ShoppingCartVM.OrderHeader.ApplicationUser.LastName;
        ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
        ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
        ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
        ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
        ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

        foreach (var cart in ShoppingCartVM.ShoppingCartList)
        {
            var basePrice = cart.Product.ProductType.OnSale ? cart.Product.ProductType.Price * 0.5m : cart.Product.ProductType.Price;
            cart.Price = GetPriceBasedOnQuantity(cart, basePrice);
            ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }

        return View(ShoppingCartVM);
    }


    [HttpPost]
    [ActionName("Summary")]
    public async Task<IActionResult> SummaryPOST(ShoppingCartVM shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        shoppingCart.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
            u => u.ApplicationUserId == userId,
            include: q => q.Include(sc => sc.Product)
                           .ThenInclude(p => p.ProductType)
        );

        if (shoppingCart.OrderHeader == null)
        {
            shoppingCart.OrderHeader = new OrderHeader();
        }

        shoppingCart.OrderHeader.OrderDate = DateTime.Now;
        shoppingCart.OrderHeader.ApplicationUserId = userId;

        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

        shoppingCart.OrderHeader.Name = applicationUser.FirstName + " " + applicationUser.LastName;
        shoppingCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
        shoppingCart.OrderHeader.StreetAddress = applicationUser.StreetAddress;
        shoppingCart.OrderHeader.City = applicationUser.City;
        shoppingCart.OrderHeader.State = applicationUser.State;
        shoppingCart.OrderHeader.PostalCode = applicationUser.PostalCode;

        shoppingCart.OrderHeader.OrderTotal = 0;

        DiscountCode discount = null;
        if (!string.IsNullOrEmpty(shoppingCart.DiscountCode?.Name))
        {
            discount = _unitOfWork.DiscountCode.Get(
               d => d.Name == shoppingCart.DiscountCode.Name && d.isActive && d.ExpirationDate > DateTime.Now);
        }

        if (discount != null)
        {
            shoppingCart.DiscountCode = discount;
        }
        foreach (var cart in shoppingCart.ShoppingCartList)
        {
            var productPrice = cart.Product.ProductType.OnSale
                ? cart.Product.ProductType.Price * 0.5m
                : cart.Product.ProductType.Price;

            productPrice = discount == null ? productPrice : productPrice * ((100 - discount.DiscountPercent) / 100);
            cart.Price = (double)GetPriceBasedOnQuantity(cart, productPrice);

            shoppingCart.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }
 
        if (applicationUser.CompanyId.GetValueOrDefault() == 0)
        {
            shoppingCart.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusPending;
            shoppingCart.OrderHeader.OrderStatus = StaticDetails.StatusPending;
        }
        else
        {
            shoppingCart.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusDelayed;
            shoppingCart.OrderHeader.OrderStatus = StaticDetails.StatusApproved;
        }

        _unitOfWork.OrderHeader.Add(shoppingCart.OrderHeader);
        _unitOfWork.Save();

        foreach (var cart in shoppingCart.ShoppingCartList)
        {
            OrderDetails orderDetails = new()
            {
                ProductId = cart.ProductId,
                OrderHeaderId = shoppingCart.OrderHeader.Id,
                Price = cart.Price,
                Count = cart.Count
            };
            _unitOfWork.OrderDetails.Add(orderDetails);
            _unitOfWork.Save();
        }

        if (applicationUser.CompanyId.GetValueOrDefault() == 0)
        {
            var domain = Request.Scheme + "://" + Request.Host.Value + "/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"cart/OrderConfirmation?id={shoppingCart.OrderHeader.Id}",
                CancelUrl = domain + "cart/index",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            // Report creation based on the order  
            await AddReport(shoppingCart.OrderHeader.Id);

            foreach (var item in shoppingCart.ShoppingCartList)
            {
 
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                         UnitAmount = (long)(item.Price * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.ProductType.Name
                        }
                    },
                    Quantity = item.Count,
 
                };
                options.LineItems.Add(sessionLineItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentID(shoppingCart.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        return RedirectToAction(nameof(OrderConfirmation), new { id = shoppingCart.OrderHeader.Id });
    }
    [HttpPost]
    public IActionResult ApplyDiscount(ShoppingCartVM shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        shoppingCart.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
            u => u.ApplicationUserId == userId,
            include: q => q.Include(sc => sc.Product).ThenInclude(p => p.ProductType)
        );

        if (shoppingCart.OrderHeader == null)
        {
            shoppingCart.OrderHeader = new OrderHeader();
        }

        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

        shoppingCart.OrderHeader.Name = applicationUser.FirstName + " " + applicationUser.LastName;
        shoppingCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
        shoppingCart.OrderHeader.StreetAddress = applicationUser.StreetAddress;
        shoppingCart.OrderHeader.City = applicationUser.City;
        shoppingCart.OrderHeader.State = applicationUser.State;
        shoppingCart.OrderHeader.PostalCode = applicationUser.PostalCode;

        shoppingCart.OrderHeader.OrderTotal = 0;

        DiscountCode discount = null;
        if (!string.IsNullOrEmpty(shoppingCart.DiscountCode?.Name))
        {
            discount = _unitOfWork.DiscountCode.Get(
               d => d.Name == shoppingCart.DiscountCode.Name && d.isActive && d.ExpirationDate > DateTime.Now);
        }

        if (discount != null)
        {
            shoppingCart.DiscountCode = discount;
        }
        foreach (var cart in shoppingCart.ShoppingCartList)
        {
            var productPrice = cart.Product.ProductType.OnSale
                ? cart.Product.ProductType.Price * 0.5m
                : cart.Product.ProductType.Price;

            productPrice = discount == null ? productPrice : productPrice * ((100 - discount.DiscountPercent) / 100);
            cart.Price = (double)GetPriceBasedOnQuantity(cart, productPrice);

            shoppingCart.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }

        return View("Summary", shoppingCart);
    }


    private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart, decimal basePrice)
    {
        if (shoppingCart.Count <= 10)
        {
            return (double)basePrice ;
        }
        else
        {
            // 10% discount
            if (shoppingCart.Count < 50)
            {
                return (double)(basePrice * 0.90m); // 10% discount
            }
            // 25% discount
            else
            {
                return (double)(basePrice * 0.75m); // 25% discount
            }
        }
    }

    public IActionResult OrderConfirmation(int id)
    {
        OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(
            u => u.Id == id,
            include: q => q.Include(o => o.ApplicationUser)
        );

        // check payment status
        if (orderHeader.PaymentStatus != StaticDetails.PaymentStatusDelayed)
        {
            var service = new SessionService();
            Session session = service.Get(orderHeader.IdSession);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.OrderHeader.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                _unitOfWork.OrderHeader.UpdateStatus(id, StaticDetails.StatusApproved, StaticDetails.PaymentStatusApproved);
                _unitOfWork.Save();
            }
            HttpContext.Session.Clear();
        }

        // delete items from the basket
        List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart
            .GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

        _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
        _unitOfWork.Save();

        // get OrderVM
        OrderVM orderVM = new OrderVM
        {
            OrderHeader = orderHeader,
            OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == id,
                include: q => q.Include(p => p.Product)
                            .ThenInclude(p => p.ProductType))
        };

        return View(orderVM);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddToBasket(ProductFormVM product)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        var productId = _unitOfWork.Product.Get(p => p.ProductTypeId == product.ProductTypeId && p.ColorId == product.ServiceId).Id;

        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == productId);

        if (cartFromDb != null)
        {
            // shopping cart exist
            cartFromDb.Count += product.NumberOfProducts;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
        }
        else
        {
            // add cart record
            var cart = new ShoppingCart()
            {
                Id = 0,
                ApplicationUserId = userId,
                Count = product.NumberOfProducts,
                ProductId = productId
            };
            _unitOfWork.ShoppingCart.Add(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(StaticDetails.SessionCart,
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
        }
        TempData["success"] = "Cart updated successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task AddReport(int orderHeaderId)
    {
        Report raport = new Report()
        {
            IssueDate = DateTime.Now,
            TotalAcusticGuitars = 0,
            TotalElectricGuitars = 0,
            TotalClasicalGuitars = 0,
            TotalOtherGuitars = 0,
            TotalBassGuitars = 0,
            TotalAmplifiers = 0,
            TotalStrings = 0,
         };
   

         var orderList = _unitOfWork.OrderDetails.GetAll(
            t => t.OrderHeaderId == orderHeaderId,
            include: q => q.Include(d => d.Product).ThenInclude(p => p.ProductType)
                            .ThenInclude(pt => pt.ProductSubCategory)
                            .ThenInclude(sc => sc.ProductCategory)
        ).ToList();

        foreach (var element in orderList)
        {
            var t = (decimal)(element.Count * element.Price); 
            var category = element.Product.ProductType.ProductSubCategory.ProductCategory.Name;

            if (category.Equals("Acustic"))
            {
                raport.TotalAcusticGuitars += t;
            }
            else if (category.Equals("Electric"))
            {
                raport.TotalElectricGuitars += t;
            }
            else if (category.Equals("Clasical"))
            {
                raport.TotalClasicalGuitars += t;
            }
            else if (category.Equals("Other Guitars"))
            {
                raport.TotalOtherGuitars += t;
            }
            else if (category.Equals("Bass"))
            {
                raport.TotalBassGuitars += t;
            }
            else if (category.Equals("Amplifiers"))
            {
                raport.TotalAmplifiers += t;
            }
            else if (category.Equals("Strings"))
            {
                raport.TotalStrings += t;
            }
        }

        _unitOfWork.Report.Add(raport);
        _unitOfWork.Save();
    }

}
