using MusicStore.Repository.IRepository;
using MusicStoreData.Data;
 
namespace MusicStore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MusicStoreContext _context;
        public IProductSubCategoryRepository ProductSubCategory { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }
        public IWebHeaderRepository WebHeader { get; private set; }
        public IManufacturerRepository Manufacturer { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IProductTypeRepository ProductType { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductImageRepository ProductImage { get; private set; }
        public ITermsAndConditionRepository TermsAndCondition { get; private set; }
        public IMoreRepository More { get; private set; }
        public IConcertRepository Concert { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IGalleryRepository Gallery { get; private set; }
        public IAboutUsRepository AboutUs { get; private set; }
        public IServiceRepository Service { get; }
        public IBlogRepository Blog { get; }
        public IAnnouncementRepository Announcement { get; }
        public ITrainingRepository Training { get; }
        public IReviewsRepository Reviews { get; }
        public IWishlistRepository Wishlist { get; }
        public IWishlistItemsRepository WishlistItems { get; }
        public IDiscountCodeRepository DiscountCode { get; }
        public IMusicStoreAddressRepository MusicStoreAddress { get; }
        public IOrderStatusesRepository OrderStatuses { get; }
        public IPaymentStatusesRepository PaymentStatuses { get; }
        public IReportRepository Report { get; }

        public UnitOfWork(MusicStoreContext context)
        {
            _context = context;
            ProductSubCategory = new ProductSubCategoryRepository(context);
            ProductCategory = new ProductCategoryRepository(context);
            WebHeader = new WebHeaderRepository(context);
            Manufacturer = new ManufacturerRepository(context);
            Company = new CompanyRepository(context);
            ShoppingCart = new ShoppingCartRepository(context);
            ApplicationUser = new ApplicationUserRepository(context);
            ProductType = new ProductTypeRepository(context);
            Product = new ProductRepository(context);
            ProductImage = new ProductImageRepository(context);
            TermsAndCondition = new TermsAndConditionRepository(context);
            More = new MoreRepository(context);
            Concert = new ConcertRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
            OrderDetails = new OrderDetailsRepository(context);
            Gallery = new GalleryRepository(context);
            AboutUs = new AboutUsRepository(context);
            Service = new ServiceRepository(context);
            Blog = new BlogRepository(context);
            Announcement = new AnnouncementRepository(context);
            Training = new TrainingRepository(context);
            Reviews = new ReviewsRepository(context);
            Wishlist = new WishlistRepository(context);
            WishlistItems = new WishlistItemRepository(context);
            DiscountCode = new DiscountCodeRepository(context);
            MusicStoreAddress = new MusicStoreAddressRepository(context);
            OrderStatuses = new OrderStatusesRepository(context);
            PaymentStatuses = new PaymentStatusesRepository(context);
            Report = new ReportRepository(context);
   

    }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
