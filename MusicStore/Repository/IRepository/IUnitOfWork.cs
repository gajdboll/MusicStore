namespace MusicStore.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProductSubCategoryRepository ProductSubCategory { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IWebHeaderRepository WebHeader { get; }
        public IManufacturerRepository Manufacturer { get;  }
        public ICompanyRepository Company { get; }
        public IShoppingCartRepository ShoppingCart { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public IProductTypeRepository ProductType { get; }
        public IProductRepository Product { get; }
        public IProductImageRepository ProductImage { get; }
        public ITermsAndConditionRepository TermsAndCondition { get; }
        public IMoreRepository More { get; }
        public IConcertRepository Concert { get; }
        public IOrderHeaderRepository OrderHeader { get; }
        public IOrderDetailsRepository OrderDetails { get; }
        public IGalleryRepository Gallery { get; }
        public IAboutUsRepository AboutUs { get; }
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
        public IReportRepository Report{ get; }


        void Save();
    }
}
