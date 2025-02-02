using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using MusicStore.Repository.IRepository;
using MusicUtilities;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
 
namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                HttpContext.Session.SetInt32(StaticDetails.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
            }
            return View();
        }
        // 1 get all items and allows to list that on the main sceen - on first load
        public async Task<IActionResult> ShowProductCategories(int id)
        {
            var categories = _unitOfWork.ProductCategory.GetAll(
               p => p.WebHeaderId == id && p.isActive,
               include: q => q.Include(p => p.ProductSubCategories))
           .OrderBy(k => k.Position);
            ViewBag.WebHeaderId = id;

            return View(await Task.FromResult(categories));
        }
        // 2 that endpoint is run when first category like Electric etc is selected - List of Categories returned from db
        public async Task<IActionResult> ShowProductSubCategories(int id)
        {
            var category = _unitOfWork.ProductCategory.Get(p => p.Id == id);
            var subCategories = _unitOfWork.ProductSubCategory.GetAll(
               p => p.ProductCategoryId == id && p.isActive,
               include: q => q.Include(p => p.Products))
           .OrderBy(k => k.Position);
            ViewData["CategoryName"] = category.Name;
            ViewData["PreviousCategoryId"] = category.Id;
            return View(await Task.FromResult(subCategories));
        }
        // 3 that endpoint is run when first category is selected from list of categories - return list of subcategories
        public async Task<IActionResult> ShowProductsFromSubCategory(int id, string manufacturer = null)
        {
            var subCategory = _unitOfWork.ProductSubCategory.Get(p => p.Id == id && p.isActive);
            var category = _unitOfWork.ProductCategory.Get(p => p.Id == subCategory.ProductCategoryId && p.isActive);
            var productsQuery = _unitOfWork.ProductType.GetAll(p => p.isActive,
                include: q => q
                    .Include(p => p.ProductSubCategory)
                        .ThenInclude(psc => psc.ProductCategory)
                        .Include(o=>o.ProductImages))                       
                .Where(p => p.ProductSubCategoryId == id && p.isActive);

            ViewData["SubCategoryName"] = subCategory.Name;
            ViewData["SubCategoryId"] = subCategory.Id;
            ViewData["CategoryName"] = category.Name;
            ViewData["CategoryId"] = category.Id;

            if (!string.IsNullOrEmpty(manufacturer))
            {
                productsQuery = productsQuery.Where(p => p.Manufacturer.Name == manufacturer);
            }

            var manufacturers = _unitOfWork.Manufacturer.GetAll(p => p.isActive);
            ViewData["Manufacturers"] = manufacturers;
            ViewData["SelectedManufacturer"] = manufacturer;

            ViewData["SubCategoryName"] = subCategory.Name;
            ViewData["SubCategoryId"] = subCategory.Id;

            return View(productsQuery);
        }
        //4 that endpoint return single product type selected by specific id type from the list
        public async Task<IActionResult> ShowProduct(int id)
        {
            var product = _unitOfWork.ProductType.Get(
                p => p.Id == id && p.isActive,
                include: q => q
                    .Include(p => p.Manufacturer)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductSubCategory)
                        .ThenInclude(psc => psc.ProductCategory)
                    .Include(p => p.Products)
                        .ThenInclude(pr => pr.Color));

            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = product.Id;
            ViewData["SubCategoryId"] = product.ProductSubCategoryId;
            ViewData["CategoryId"] = product.ProductSubCategory.ProductCategoryId;

            ViewBag.ProductTypeId = id;
            return View(product);
        }
        public IActionResult OnSaleProducts()
        {
            var onSaleProductTypes = _unitOfWork.ProductType.GetAll(
                //  filter: pt => pt.OnSale, // Filter products that are on sale
                include: q => q
                    .Include(pt => pt.ProductImages)
                    .Include(pt => pt.Manufacturer)
                    .Include(pt => pt.ProductSubCategory)
            );

            if (!onSaleProductTypes.Any())
            {
                ViewBag.NoProductsMessage = "No products are currently on sale.";
            }

            return View(onSaleProductTypes);
        }
        public async Task<IActionResult> ShowTermsAndCondition(int id)
        {
            var term = _unitOfWork.TermsAndCondition.Get(
                t => t.Id == id
            );

            if (term == null)
            {
                return NotFound();
            }

            var allTerms = _unitOfWork.TermsAndCondition.GetAll()
                .Where(t => t.isActive);

            ViewBag.TermsList = await Task.FromResult(allTerms);

            return View(await Task.FromResult(term));
        }

        public async Task<IActionResult> ShowAboutUs()
        {
            return View(_unitOfWork.AboutUs.GetAll(t => t.isActive));
        }
        public async Task<IActionResult> ShowConcerts()
        {
            return View(_unitOfWork.Concert.GetAll(t => t.isActive));
        }
        public async Task<IActionResult> ShowServices()
        {
            return View(_unitOfWork.Service.GetAll(t => t.isActive));
        }
        public async Task<IActionResult> ShowBlogs()
        {
            return View(_unitOfWork.Blog.GetAll(t => t.isActive));

        }
        public async Task<IActionResult> ShowContactUs()
        {
            return View(_unitOfWork.MusicStoreAddress.GetAll(t => t.isActive,
                 include: q => q
                    .Include(p => p.OpeningHours)));
        }
        public async Task<IActionResult> ShowAnnouncements()
        {
            return View(_unitOfWork.Announcement.GetAll(t => t.isActive));
        }
        public async Task<IActionResult> ShowTraining()
        {
            return View(_unitOfWork.Training.GetAll(t => t.isActive));
        }
        public async Task<IActionResult> ShowGallery()
        {
            return View(_unitOfWork.Gallery.GetAll().Where(p => p.isActive).ToList());

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // Report ,ethod allowing us to create chart 
        public IActionResult ReportChart(DateTime? from, DateTime? end)
        {
            DateTime? fromDate = from ?? new DateTime(2024, 1, 1);
            DateTime? endDate = end ?? DateTime.Now.Date.AddDays(1);

            var raporty = _unitOfWork.Report.GetAll(t => t.IssueDate >= fromDate && t.IssueDate <= endDate);

            var labels = new List<string>
    {
        "Acustic", "Electric", "Clasical", "Other", "Bass", "Amps", "Strings", "Overall"
    };

            decimal? totalAcustic = 0;
            decimal? totalElectric = 0;
            decimal? totalClasical = 0;
            decimal? totalOther = 0;
            decimal? totalBass = 0;
            decimal? totalAmps = 0;
            decimal? totalStrings = 0;
            decimal? TotalTogether = 0;

            foreach (var raport in raporty)
            {
                totalAcustic += raport.TotalAcusticGuitars;
                totalElectric += raport.TotalElectricGuitars;
                totalClasical += raport.TotalClasicalGuitars;
                totalOther += raport.TotalOtherGuitars;
                totalBass += raport.TotalBassGuitars;
                totalAmps += raport.TotalAmplifiers;
                totalStrings += raport.TotalStrings;

                // Sum of all categories together
                TotalTogether += raport.TotalAcusticGuitars + raport.TotalElectricGuitars + raport.TotalClasicalGuitars
                    + raport.TotalOtherGuitars + raport.TotalBassGuitars + raport.TotalAmplifiers + raport.TotalStrings;
            }

            //Data for the chart
            var chartData = new
            {
                labels = labels,
                datasets = new[]
                {
            new
            {
                label = $"Your revenue from {fromDate.GetValueOrDefault().ToShortDateString()} to {endDate.GetValueOrDefault().ToShortDateString()}",
                data = new decimal?[] { totalAcustic, totalElectric, totalClasical, totalOther, totalBass, totalAmps, totalStrings, TotalTogether },
                backgroundService = new[]
                {
                    "rgba(43, 112, 255, 0.2)",  // Blue
                    "rgba(112, 43, 255, 0.2)",  // Purple
                    "rgba(255, 43, 112, 0.2)",  // Pink
                    "rgba(112, 255, 43, 0.2)",  // Green
                    "rgba(255, 206, 86, 0.2)",  // Yellow
                    "rgba(75, 192, 192, 0.2)",  // Teal
                    "rgba(153, 102, 255, 0.2)", // Light Purple
                    "rgba(255, 159, 64, 0.2)"   // Orange
                },
                borderService = new[]
                {
                    "rgba(43, 112, 255, 1)",  // Blue
                    "rgba(112, 43, 255, 1)",  // Purple
                    "rgba(255, 43, 112, 1)",  // Pink
                    "rgba(112, 255, 43, 1)",  // Green
                    "rgba(255, 206, 86, 1)",  // Yellow
                    "rgba(75, 192, 192, 1)",  // Teal
                    "rgba(153, 102, 255, 1)", // Light Purple
                    "rgba(255, 159, 64, 1)"   // Orange
                },
                borderWidth = 1
            }
        }
            };

            return View((object)JsonConvert.SerializeObject(chartData));
        }
 
        [Route("Home/NotFound")]
        public IActionResult NotFound(int statusCode)
        {
            ViewData["StatusCode"] = statusCode;
            return View();
        }
    }
}