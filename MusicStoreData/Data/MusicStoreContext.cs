using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Models.Basket;
using MusicStoreData.Models.BusinesLogic;
using MusicStoreData.Models.CMS;
using MusicStoreData.Models.ShoppingCart;
using MusicStoreData.Models.Store;
using MusicStoreData.Models.Users;
  
namespace MusicStoreData.Data
{
    public class MusicStoreContext : IdentityDbContext<IdentityUser>
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            : base(options)
        {
        }
        public DbSet<WebHeaders>? WebHeaders { get; set; }
        public DbSet<Color>? Color { get; set; }
        public DbSet<Product>? Product { get; set; }
        public DbSet<ProductImage>? ProductImage { get; set; }
        public DbSet<ProductCategory>? ProductCategory { get; set; }
        public DbSet<ProductSubCategory>? ProductSubCategory { get; set; }
        public DbSet<ProductType>? ProductType { get; set; }
        public DbSet<Manufacturer>? Manufacturer { get; set; }
        public DbSet<Training>? Training { get; set; }
        public DbSet<Service>? Service { get; set; }
        public DbSet<Announcement>? Announcement { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<TechInfo> TechInfos { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<AdminPolicies> AdminPolicies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<PaymentStatuses> PaymentStatuses { get; set; }
        public DbSet<OrderStatuses> OrderStatuses { get; set; }
        public DbSet<DiscountCode> DiscountCode { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<NewsAndArticles>? NewsAndArticles { get; set; }
        public DbSet<SocialMedia>? SocialMedia { get; set; }
        public DbSet<Newslatter>? Newslatter { get; set; }
        public DbSet<Concert>? Concert { get; set; }
        public DbSet<Gallery>? Gallery { get; set; }
        public DbSet<AboutUs>? AboutUs { get; set; }
        public DbSet<CustomerQuery>? CustomerQuery { get; set; }
        public DbSet<Report>? Report { get; set; }
        public DbSet<TermsAndCondition> TermsAndCondition { get; set; }
        public DbSet<ControlActions> ControlActions { get; set; }
        public DbSet<BookmarkedProduct> BookmarkedProduct { get; set; }
        public DbSet<MusicStoreAddress>? MusicStoreAddress { get; set; }
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Company>? Company { get; set; }
        public DbSet<More>? More { get; set; }
        public DbSet<Reviews>? Reviews { get; set; }
        public DbSet<Subscription>? Subscription { get; set; }
        public DbSet<Subscribers>? Subscribers { get; set; }



        // Below is the data with all the db examples in case db fails 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShoppingCart>().Property(c => c.Id).ValueGeneratedOnAdd();


          //  modelBuilder.Entity<Subscription>().HasData(
          //    new Subscription { Id = 1, Name = "Confesor123@gmail.com", Descriptions = "Sign Up For Newsletter", MoreInfo = "Sign up for our mailing list to get the latest updates and offers", ButtonName = "SUBSCRIBE", isActive = true, Position = 1 }
          //  );

          //  // Concerts
          //  modelBuilder.Entity<Concert>(entity =>
          //  {
          //      entity.HasData(
          //          new Concert
          //          {
          //              Id = 1,
          //              City = "Leszons",
          //              Location = "Pod Buda 2",
          //              MusicType = "Kowalski Rock",
          //              OpeningTime = new DateTime(2024, 09, 12, 0, 0, 0),
          //              Image = "http://localhost/Images/png/concerts/betoniary.webp",
          //              isActive = true,
          //              Name = "Rozujane Betoniary",
          //              Position = 1,
          //              Descriptions = "A band known for mixing concrete rhythms with hardcore vocals—watch out for flying bricks!"
          //          },
          //          new Concert
          //          {
          //              Id = 2,
          //              City = "Kowalikowo",
          //              Location = "Meksykańska",
          //              MusicType = "Emigranci Punk-Folk",
          //              OpeningTime = new DateTime(2024, 08, 11, 10, 0, 0),
          //              Image = "http://localhost/Images/png/concerts/santa.webp",
          //              isActive = true,
          //              Name = "Cool Santa Claus",
          //              Position = 2,
          //              Descriptions = "A band with a fun, festive vibe featuring a cool, modern twist on Christmas classics."
          //          },
          //          new Concert
          //          {
          //              Id = 3,
          //              City = "Ernestów",
          //              Location = "Kolibry",
          //              MusicType = "Kazakhstan Pop",
          //              OpeningTime = new DateTime(2024, 09, 05, 0, 0, 0),
          //              Image = "http://localhost/Images/png/concerts/chopek.webp",
          //              isActive = true,
          //              Name = "Beloved Dog Chopek",
          //              Position = 3,
          //              Descriptions = "A wholesome and playful band centered around a loyal and beloved dog mascot."
          //          },
          //          new Concert
          //          {
          //              Id = 4,
          //              City = "Kalisz",
          //              Location = "Karma",
          //              MusicType = "Jazz",
          //              OpeningTime = new DateTime(2024, 09, 19, 0, 0, 0),
          //              Image = "http://localhost/Images/png/concerts/jackets.webp",
          //              isActive = true,
          //              Name = "Edzie",
          //              Position = 4,
          //              Descriptions = "Edzie brings the chill—this jazz quartet is so smooth, even their instruments wear sunglasses."
          //          }
          //      );
          //  });
          //  // about-Us
          //  modelBuilder.Entity<AboutUs>().HasData(
          //      new AboutUs
          //      {
          //          Id = 1,
          //          AdditionalParagraph = "We are a team of experienced musicians and specialists dedicated to helping customers find the best gear for their needs.",
          //          PhotoUrl = "http://localhost/Images/png/aboutUs/AboutStore.webp",
          //          isActive = true,
          //          Name = "About Our Store",
          //          Position = 1,
          //          Descriptions = "Our music store is a place where passion for music meets the highest quality musical equipment. We offer a wide range of musical instruments, sound equipment, and accessories that meet the expectations of both amateurs and professionals."
          //      },
          //      new AboutUs
          //      {
          //          Id = 2,
          //          AdditionalParagraph = "Our philosophy is based on continuous growth and improvement in the ever-changing world of music.",
          //          PhotoUrl = "http://localhost/Images/png/gallery/headphones.webp",
          //          isActive = true,
          //          Name = "Our History",
          //          Position = 2,
          //          Descriptions = "Our history dates back many years when we decided to create a place where every music lover can find something for themselves. We started as a small instrument store, and today we are one of the leading suppliers of musical equipment in the region."
          //      }
          //  );
          //  //ControlActions
          //  modelBuilder.Entity<ControlActions>().HasData(
          //      new ControlActions
          //      {
          //          Id = 12,
          //          isActive = true,
          //          Name = "Company",
          //          Position = 4,
          //          Descriptions = "Manage Company",
          //          ControlSection = "test"
          //      },
          //      new ControlActions
          //      {
          //          Id = 13,
          //          isActive = true,
          //          Name = "Chart Report",
          //          Position = 5,
          //          Descriptions = "Chart Report",
          //          ControlSection = "test",
          //          ControlerUrl = ".. / .. /./ png / more / default.png"
          //      },
          //      new ControlActions
          //      {
          //          Id = 15,
          //          isActive = true,
          //          Name = "Order",
          //          Position = 3,
          //          Descriptions = "Manage Order",
          //          ControlSection = "test",
          //          ControlerUrl = ".. / .. /./ png / more / default.png"

          //      },
          //      new ControlActions
          //      {
          //          Id = 20,
          //          isActive = true,
          //          Name = "User",
          //          Position = 1,
          //          Descriptions = "Manage User",
          //          ControlSection = "test",
          //          ControlerUrl = ".. / .. /./ png / more / default.png"

          //      },
          //      new ControlActions
          //      {
          //          Id = 21,
          //          isActive = true,
          //          Name = "/Account/Register",
          //          Position = 2,
          //          Descriptions = "Create User",
          //          ControlSection = "test",
          //          ControlerUrl = ".. / .. /./ png / more / default.png"

          //      }
          //  );

          //  // T&C
          //  modelBuilder.Entity<TermsAndCondition>().HasData(
          //    new TermsAndCondition { Id = 1, Name = "Terms and Conditions", isActive = true, Position = 1, TermsPhotoUrl = "http://localhost/Images/png/terms/termsAndConditions.webp", Descriptions = "Welcome to MusicStore" },
          //    new TermsAndCondition { Id = 2, Name = "Cookies", isActive = true, Position = 2, TermsPhotoUrl = "http://localhost/Images/png/terms/cookies.webp", Descriptions = "We employ the use of cookies. By accessing MusicStore, you agreed to use cookies in agreement with the MusicStore's Privacy Policy." },
          //    new TermsAndCondition { Id = 3, Name = "License", isActive = true, Position = 3, TermsPhotoUrl = "http://localhost/Images/png/terms/Lincense.webp", Descriptions = "Unless otherwise stated, MusicStore and/or its licensors own the intellectual property rights for all material on MusicStore. All intellectual property rights are reserved. You may access this from MusicStore for your own personal use subjected to restrictions set in these terms and conditions." },
          //    new TermsAndCondition { Id = 4, Name = "Purchases", isActive = true, Position = 4, TermsPhotoUrl = "http://localhost/Images/png/terms/purchase.webp", Descriptions = "By placing an order, you are offering to purchase a product subject to the following terms and conditions. All orders are subject to availability and confirmation of the order price. Dispatch times may vary according to availability and any guarantees or representations made as to delivery times are limited to the United States and are subject to any delays resulting from postal delays or force majeure for which we will not be responsible." },
          //    new TermsAndCondition { Id = 5, Name = "Returns and Refunds", isActive = true, Position = 5, TermsPhotoUrl = "http://localhost/Images/png/terms/refunds.webp", Descriptions = "We aim to offer a smooth return process. If you are not satisfied with your purchase, please return the product in its original condition within 30 days of purchase for a full refund. Please note that we do not cover the return shipping costs unless the product is faulty or damaged." },
          //    new TermsAndCondition { Id = 6, Name = "Online Sales", isActive = true, Position = 6, TermsPhotoUrl = "http://localhost/Images/png/terms/onlineSales.webp", Descriptions = "MusicStore sells products both online and in-store. Online sales are subject to the same terms and conditions as in-store purchases. Prices online may differ from in-store due to special promotions. We reserve the right to adjust prices at any time." },
          //    new TermsAndCondition { Id = 7, Name = "Privacy Policy", isActive = true, Position = 7, TermsPhotoUrl = "http://localhost/Images/png/terms/policy.webp", Descriptions = "Your privacy is important to us. Please review our Privacy Policy for details on how we collect, use, and protect your personal information." },
          //    new TermsAndCondition { Id = 8, Name = "Governing Law", isActive = true, Position = 8, TermsPhotoUrl = "http://localhost/Images/png/terms/governing.webp", Descriptions = "These terms and conditions are governed by and construed in accordance with the laws of the State of California and you irrevocably submit to the exclusive jurisdiction of the courts in that State or location." },
          //    new TermsAndCondition { Id = 9, Name = "Changes to Terms", isActive = true, Position = 9, TermsPhotoUrl = "http://localhost/Images/png/terms/changes.png.webp", Descriptions = "MusicStore reserves the right to amend these terms and conditions at any time. Any changes will be posted on this page and your continued use of the site will signify your acceptance of any adjustment to these terms." }
          //);
          //  //more
          //  modelBuilder.Entity<More>().HasData(
          //    new More { Id = 1, Name = "More", isActive = true, Position = 1, Descriptions = "More Info", MoreIcon = "http://localhost/Images/png/more/more.webp" },
          //    new More { Id = 2, Name = "About Us", isActive = true, Position = 2, Descriptions = "About Us", MoreIcon = "http://localhost/Images/png/more/about.webp" },
          //    new More { Id = 3, Name = "Contact", isActive = true, Position = 3, Descriptions = "Contact", MoreIcon = "http://localhost/Images/png/more/contact.webp" },
          //    new More { Id = 4, Name = "Blogs", isActive = true, Position = 4, Descriptions = "Blogs", MoreIcon = "http://localhost/Images/png/more/blogs.webp" },
          //    new More { Id = 5, Name = "Service", isActive = true, Position = 5, Descriptions = "Service", MoreIcon = "http://localhost/Images/png/more/service.webp" },
          //    new More { Id = 6, Name = "Concerts", isActive = true, Position = 6, Descriptions = "Concerts", MoreIcon = "http://localhost/Images/png/more/concerts.webp" }
          //  );


          //  // CustomerQuery
          //  modelBuilder.Entity<CustomerQuery>().HasData(
          //      new CustomerQuery { Id = 1, Email = "john.doe@example.com", ContactNumber = "+1-555-1234", isActive = true, Name = "John Doe", Position = 1, Descriptions = "Inquiry about guitar availability", IsAnswered = false },
          //      new CustomerQuery { Id = 2, Email = "jane.smith@example.com", ContactNumber = "+1-555-5678", isActive = true, Name = "Jane Smith", Position = 2, Descriptions = "Looking for beginner guitar recommendations", IsAnswered = true },
          //      new CustomerQuery { Id = 3, Email = "michael.jordan@example.com", ContactNumber = "+1-555-9876", isActive = true, Name = "Michael Jordan", Position = 3, Descriptions = "Request for order cancellation", IsAnswered = true },
          //      new CustomerQuery { Id = 4, Email = "emily.jones@example.com", ContactNumber = "+1-555-6543", isActive = true, Name = "Emily Jones", Position = 4, Descriptions = "Issue with guitar tuning", IsAnswered = false },
          //      new CustomerQuery { Id = 5, Email = "robert.brown@example.com", ContactNumber = "+1-555-3456", isActive = true, Name = "Robert Brown", Position = 5, Descriptions = "Question about guitar accessories", IsAnswered = true }
          //  );
          //  // DiscountCode
          //  modelBuilder.Entity<DiscountCode>().HasData(
          //      new DiscountCode { Id = 1, DiscountPercent = 2, ExpirationDate = new DateTime(2025, 12, 31), isActive = true, Name = "WELCOME", Position = 1, Descriptions = "Welcome discount for new users", DateAdded = DateTime.Now },
          //      new DiscountCode { Id = 2, DiscountPercent = 5, ExpirationDate = new DateTime(2026, 01, 15), isActive = true, Name = "HOLIDAY", Position = 2, Descriptions = "Holiday season discount", DateAdded = DateTime.Now },
          //      new DiscountCode { Id = 3, DiscountPercent = 7, ExpirationDate = new DateTime(2025, 11, 30), isActive = true, Name = "LOYAL", Position = 3, Descriptions = "Loyal customer discount", DateAdded = DateTime.Now },
          //      new DiscountCode { Id = 4, DiscountPercent = 10, ExpirationDate = new DateTime(2025, 12, 01), isActive = true, Name = "FUN", Position = 4, Descriptions = "Special sale discount", DateAdded = DateTime.Now },
          //      new DiscountCode { Id = 5, DiscountPercent = 15, ExpirationDate = new DateTime(2026, 06, 01), isActive = true, Name = "BIG15", Position = 5, Descriptions = "Big discount for high spenders", DateAdded = DateTime.Now }
          //  );

          //  // Gallery Entries
          //  modelBuilder.Entity<Gallery>().HasData(
          //      new Gallery { Id = 1, PictureUrl = "http://localhost/Images/png/gallery/vinyl.webp", isActive = false, Name = "Customer browsing vinyl records", Position = 1, Descriptions = "A scene in a stylish store where a customer passionately browses through a collection of vinyl records on a shelf, with other sections of the store featuring instruments visible in the background." },
          //      new Gallery { Id = 2, PictureUrl = "http://localhost/Images/png/gallery/meetings.webp", isActive = true, Name = "Musicians meeting", Position = 2, Descriptions = "Two or three musicians talking at a guitar stand, with one holding an acoustic guitar and trying to play it, emphasizing the social aspect of the store." },
          //      new Gallery { Id = 3, PictureUrl = "http://localhost/Images/png/gallery/headphones.webp", isActive = true, Name = "Testing audio equipment", Position = 3, Descriptions = "A customer sitting comfortably in a chair, testing a pair of high-end headphones or speakers with a satisfied expression, surrounded by audio equipment on the shelves." },
          //      new Gallery { Id = 4, PictureUrl = "http://localhost/Images/png/gallery/acustic.webp", isActive = true, Name = "Store employee advising a customer", Position = 4, Descriptions = "A friendly employee helping a customer choose the right guitar, possibly allowing them to test it out, with colorful amplifiers and other instruments visible in the background." },
          //      new Gallery { Id = 5, PictureUrl = "http://localhost/Images/png/gallery/piano.webp", isActive = true, Name = "Young artist at a piano", Position = 5, Descriptions = "A young person sitting at a digital piano, playing a melody with a look of fascination, surrounded by musical notes and accessories." },
          //      new Gallery { Id = 6, PictureUrl = "http://localhost/Images/png/gallery/rehearsal.webp", isActive = true, Name = "Relaxing listening area", Position = 6, Descriptions = "A corner in the store with comfortable chairs where customers can quietly listen to music from a turntable or other sources, with warm lighting and an elegant design." }
          //  );
          //  // NewsAndArticles Entries
          //  modelBuilder.Entity<NewsAndArticles>().HasData(
          //      new NewsAndArticles { Id = 1, Position = 1, isActive = true, Name = "How become master", Descriptions = "Unlock your full potential as a music producer with these expert tips.", Author = "Krzysztof Jerzyna", BlogImage = "http://localhost/Images/png/news/nauka2.png", BtnName = "Get In There", NewsInfo = "Get into the world of professional music production with these insights." },
          //      new NewsAndArticles { Id = 2, Position = 2, isActive = true, Name = "Fixing Guitar", Descriptions = "Keep your guitar in top playing condition with our guide.", Author = "Olaf Lubaszenko", BlogImage = "http://localhost/Images/png/news/fix2.png", BtnName = "Learn More", NewsInfo = "In this blog, we’ll guide you through the most common guitar maintenance tips." },
          //      new NewsAndArticles { Id = 3, Position = 3, isActive = true, Name = "The Best School", Descriptions = "Discover the top music schools that can shape your future.", Author = "Edzio Kozecki", BlogImage = "http://localhost/Images/png/news/school2.png", BtnName = "Enter", NewsInfo = "This blog will explore some of the world’s leading music schools and how they can help you grow." }
          //  );
          //  // Newsletter Entries
          //  modelBuilder.Entity<Newslatter>().HasData(
          //      new Newslatter { Id = 1, Position = 3, NewslatterButton = "SUBSCRIBE", BackGroundImage = "//ap-melody.myshopify.com/cdn/shop/files/bg-box.webp", Placeholder = "Join our mailing list", isActive = true, Name = "Sign Up For Newsletter", Descriptions = "Sign up for our mailing list to get latest updates and offers." }
          //  );
          //  modelBuilder.Entity<MusicStoreAddress>().HasData(
          //      new MusicStoreAddress
          //      {
          //          Id = 1,
          //          Address = "Zdrojowa 45",
          //          Address2 = "Kynica",
          //          PhoneNumber = "+48 188 45648 454",
          //          EmailAdres = "musiclong@info.pl",
          //          isActive = true,
          //          Name = "Kapma Music",
          //          Position = 1,
          //          Descriptions = "Lets Rockin Together",
          //          IsItCurrentStore = true,
          //          Country = "Poland",
          //          PostCode = "45-885"
          //      }
          //  );
          //  modelBuilder.Entity<Announcement>().HasData(
          //      new Announcement
          //      {
          //          Id = 4,
          //          PictureUrl = "http://localhost/Images/png/Thewall/BassistWanted.webp",
          //          MoreInfo = "Bassist Wanted",
          //          FullNameContact = "Andrew G",
          //          PhoneContact = "+48 506 987 652",
          //          EmailContact = "BassistWanted@gmail.com",
          //          isActive = true,
          //          Name = "Bassist Wanted for Local Rock Band",
          //          Position = 1,
          //          Descriptions = "Looking for a talented bassist to join our established local rock band. We play a mix of classic and modern rock and are gearing up for some exciting gigs in the coming months. Rehearsals are twice a week in [City Name]. If you're passionate about music and ready to commit, we'd love to hear from you!"
          //      },
          //      new Announcement
          //      {
          //          Id = 5,
          //          PictureUrl = "http://localhost/Images/png/Thewall/lessons.webp",
          //          MoreInfo = "Guitar Lessons",
          //          FullNameContact = "Jack Sparrow",
          //          PhoneContact = "+44 598 7985 56",
          //          EmailContact = "lessons@gmail.com",
          //          isActive = true,
          //          Name = "Guitar Lessons Available - All Skill Levels",
          //          Position = 2,
          //          Descriptions = "Whether you're a beginner or looking to hone your guitar skills, we're offering personalized guitar lessons at [Store Name]. Our experienced instructors can teach you everything from basic chords to advanced techniques. Flexible scheduling and competitive rates. Start your musical journey with us today!"
          //      },
          //      new Announcement
          //      {
          //          Id = 9,
          //          PictureUrl = "http://localhost/Images/png/Thewall/vinyl.webp",
          //          MoreInfo = "Vinyl Records Wanted",
          //          FullNameContact = "Kris G",
          //          PhoneContact = "+35385 134 9874",
          //          EmailContact = "vinyl@gmail.com",
          //          isActive = true,
          //          Name = "Vintage Vinyl Records Wanted",
          //          Position = 3,
          //          Descriptions = "Are you looking to sell your collection of vintage vinyl records? We're interested in buying! We offer fair prices for rare and collectible records. Bring your collection to [Store Name] or contact us to schedule an appraisal."
          //      }
          //  );

          //  // Opening Hours Entries
          //  modelBuilder.Entity<OpeningHours>().HasData(
          //      new OpeningHours { Id = 1, DayOfWeek = "Monday - Friday", MusicStoreAddressId = 1, Openhours = "10:00 AM - 6:00 PM" },
          //      new OpeningHours { Id = 2, DayOfWeek = "Saturday", MusicStoreAddressId = 1, Openhours = "10:00 AM - 2:00 PM" },
          //      new OpeningHours { Id = 3, DayOfWeek = "Sunday", MusicStoreAddressId = 1, Openhours = "Closed" }
          //  );
          //  // Service Entries
          //  modelBuilder.Entity<Service>().HasData(
          //      new Service { Id = 5, PictureUrl = "http://localhost/Images/png/service/service.webp", MoreInfo = "From 50 €/h", isActive = true, Name = "Instrument Repair and Maintenance", Position = 1, Descriptions = "This service involves repairing and maintaining musical instruments to ensure they remain in good condition." },
          //      new Service { Id = 6, PictureUrl = "http://localhost/Images/png/service/lessons.webp", MoreInfo = "From 50 €/h", isActive = true, Name = "Music Lessons", Position = 2, Descriptions = "Music stores often offer private or group lessons for learning various musical instruments." },
          //      new Service { Id = 9, PictureUrl = "http://localhost/Images/png/service/settingUp.webp", MoreInfo = "From 50 €/h", isActive = true, Name = "Instrument Rental", Position = 3, Descriptions = "For customers who need an instrument temporarily, rental options are available." },
          //      new Service { Id = 10, PictureUrl = "http://localhost/Images/png/service/audio.webp", MoreInfo = "From 50 €/h", isActive = true, Name = "Audio Equipment Setup and Consultation", Position = 4, Descriptions = "Music stores can provide professional setup and consultation for audio equipment." }
          //  );
          //  // SocialMedia Entries
          //  modelBuilder.Entity<SocialMedia>().HasData(
          //      new SocialMedia { Id = 1, SocialMediaImageUrl = "http://localhost/Images/png/socialMedia/fb.png", SocialMoreInfo = null, isActive = true, Name = "Facebook", Position = 1, Descriptions = "facebook.com" },
          //      new SocialMedia { Id = 2, SocialMediaImageUrl = "http://localhost/Images/png/socialMedia/insta.png", SocialMoreInfo = "Insta", isActive = true, Name = "Instagram", Position = 2, Descriptions = "instagram.com" },
          //      new SocialMedia { Id = 3, SocialMediaImageUrl = "http://localhost/Images/png/socialMedia/pin.png", SocialMoreInfo = "Pinterest", isActive = true, Name = "Pinterest", Position = 3, Descriptions = "pinterest.com" },
          //      new SocialMedia { Id = 4, SocialMediaImageUrl = "http://localhost/Images/png/socialMedia/youtube.png", SocialMoreInfo = "YT", isActive = true, Name = "Youtube", Position = 4, Descriptions = "youtube.com" }
          //  );
          //  // Subscribers Entries
          //  modelBuilder.Entity<Subscribers>().HasData(
          //      new Subscribers { Id = 1001, isActive = true, Name = "john.doe@example.com", Position = 1, Descriptions = "Loyal customer since 2021" },
          //      new Subscribers { Id = 1002, isActive = true, Name = "jane.smith@musicworld.com", Position = 2, Descriptions = "Joined for exclusive offers" },
          //      new Subscribers { Id = 1003, isActive = true, Name = "alex.morris@audiolovers.com", Position = 3, Descriptions = "Subscribed for newsletter" },
          //      new Subscribers { Id = 1004, isActive = false, Name = "chris.johnson@domain.net", Position = 4, Descriptions = "Subscription paused" },
          //      new Subscribers { Id = 1005, isActive = true, Name = "emily.white@domain.com", Position = 5, Descriptions = "New subscriber for updates" }
          //  );
          //  // Subscription Entries
          //  modelBuilder.Entity<Subscription>().HasData(
          //      new Subscription { Id = 1, ButtonName = "SUBSCRIBE", MoreInfo = "Remember you can rely on Special offers, discounts, and updates.", isActive = true, Name = "Sign Up For Newsletter", Position = 1, Descriptions = "Sign up for our mailing list to get the latest updates and special offers." }
          //   );
           

          //  // Training Entries
          //  modelBuilder.Entity<Training>().HasData(
          //      new Training
          //      {
          //          Id = 1,
          //          PictureUrl = "http://localhost/Images/png/training/guitar.webp",
          //          MoreInfo = "This post will cover the essential skills every new guitarist should learn.",
          //          Position = 1,
          //          Descriptions = "Start your musical journey with our beginner guitar course designed to get you playing in no time!"
          //      },
          //      new Training
          //      {
          //          Id = 2,
          //          PictureUrl = "http://localhost/Images/png/training/mastering.webp",
          //          MoreInfo = "This post will dive into advanced topics such as mixing techniques.",
          //          isActive = true,
          //          Name = "Advanced Mixing Techniques for Producers",
          //          Position = 2,
          //          Descriptions = "Take your music production skills to the next level with professional mixing techniques."
          //      },
          //      new Training
          //      {
          //          Id = 3,
          //          PictureUrl = "http://localhost/Images/png/training/singing.webp",
          //          MoreInfo = "In this post, we’ll cover essential vocal exercises for singers.",
          //          isActive = true,
          //          Name = "Vocal Training for Singers: Techniques to Improve Your Voice",
          //          Position = 3,
          //          Descriptions = "Unlock the full potential of your voice with our vocal training program designed for all skill levels."
          //      },
          //      new Training
          //      {
          //          Id = 4,
          //          PictureUrl = "http://localhost/Images/png/training/piano.webp",
          //          MoreInfo = "This blog will outline a step-by-step learning path for piano players.",
          //          isActive = true,
          //          Name = "How to Play the Piano: From Beginner to Intermediate",
          //          Position = 4,
          //          Descriptions = "Learn how to play the piano with our structured lessons from beginner to intermediate levels."
          //      },
          //      new Training
          //      {
          //          Id = 5,
          //          PictureUrl = "http://localhost/Images/png/training/mixing.webp",
          //          MoreInfo = "This post will explore the fundamental skills every DJ needs to master.",
          //          isActive = true,
          //          Name = "DJ Training: Learn the Art",
          //          Position = 5,
          //          Descriptions = "Discover the art of DJing with our comprehensive mixing and scratching tutorials."
          //      }
          //      );
 

            

          //  // WebHeaders
          //  modelBuilder.Entity<WebHeaders>().HasData(
          //      new WebHeaders { Id = 1, Name = "GUITARS", isActive = true, Position = 1, ImageUrl = "http://localhost/Images/png/webHeaders/guitars.webp", Descriptions = "All Kinds of guitars" },
          //      new WebHeaders { Id = 2, Name = "Guitar Equipment", isActive = true, Position = 2, ImageUrl = "http://localhost/Images/png/webHeaders/eq.webp", Descriptions = "All types of equipement" }
          //     );
          //  //// ProductCategory
          //  //modelBuilder.Entity<ProductCategory>().HasData(
          //  //    new ProductCategory { Id = 1, Name = "Electric Guitars", isActive = true, Position = 1, WebHeaderId = 1, CategoryImageUrl = "../.././png/category/electric.png" },
          //  //    new ProductCategory { Id = 2, Name = "Clasical Guitars", isActive = true, Position = 2, WebHeaderId = 1, CategoryImageUrl = "../.././png/category/acu-electric.png" },
          //  //    new ProductCategory { Id = 3, Name = "Acustic Guitars", isActive = true, Position = 3, WebHeaderId = 1, CategoryImageUrl = "../.././png/category/acustic.png" },
          //  //    new ProductCategory { Id = 4, Name = "Bass Guitars", isActive = true, Position = 4, WebHeaderId = 1, CategoryImageUrl = "../.././png/category/bass.png" },
          //  //    new ProductCategory { Id = 5, Name = "Other Guitars", isActive = true, Position = 5, WebHeaderId = 1, CategoryImageUrl = "../.././png/category/other.png" },

          //  //    new ProductCategory { Id = 6, Name = "Amplifiers", isActive = true, Position = 6, WebHeaderId = 2, CategoryImageUrl = "../.././png/category/amp.png" },
          //  //    new ProductCategory { Id = 6, Name = "Packs", isActive = true, Position = 7, WebHeaderId = 2, CategoryImageUrl = "../.././png/category/strings.png" }
          //  //);

          //  // ProductSubCategory
          //  //modelBuilder.Entity<ProductSubCategory>().HasData(
          //  //    new ProductSubCategory { Id = 1, Name = "Solid Body", isActive = true, Position = 1, SubCategoryImageUrl = "../.././png/subcategory/electric-clasical.png", ProductCategoryId = 1 },
          //  //    new ProductSubCategory { Id = 2, Name = "Hollow Body", isActive = true, Position = 2, SubCategoryImageUrl = "../.././png/subcategory/hollow.png", ProductCategoryId = 1 },
          //  //    new ProductSubCategory { Id = 3, Name = "Lefthanded Electric Guitar", isActive = true, Position = 3, SubCategoryImageUrl = "../.././png/subcategory/left.png", ProductCategoryId = 1 },
          //  //    new ProductSubCategory { Id = 4, Name = "Clasical Guitars", isActive = true, Position = 4, SubCategoryImageUrl = "../.././png/subcategory/clasical.png", ProductCategoryId = 2 },
          //  //    new ProductSubCategory { Id = 5, Name = "Electro-Clasical Guitars", isActive = true, Position = 5, SubCategoryImageUrl = "../.././png/subcategory/electro-clasical.png", ProductCategoryId = 2 }
          //  //);
          //  // Manufacturer
          //  modelBuilder.Entity<Manufacturer>().HasData(
          //      new Manufacturer { Id = 1, Name = "Gibson", isActive = true, Position = 1, LogoUrl = "", CountryOfOrigin = "USA", ContactNumber = "+1-480-596-9690", ManufacturerEmail = "info@fender.com", ManufactureAddress = "17600 N. Perimeter Drive, Suite 100, Scottsdale, AZ 85255, USA", Descriptions = "Known for iconic models like the Stratocaster and Telecaster, Fender has shaped rock, blues, and jazz since the 1950s." },
          //      new Manufacturer { Id = 2, Name = "Ibanez", isActive = true, Position = 2, LogoUrl = "", CountryOfOrigin = "USA", ContactNumber = "+1-800-444-2766", ManufacturerEmail = "support@gibson.com", ManufactureAddress = "209 10th Avenue South, Nashville, TN 37203, USA", Descriptions = "Famous for the Les Paul, Gibson is a top choice for rock and blues musicians, used by legends like Slash and Jimmy Page." },
          //      new Manufacturer { Id = 3, Name = "Fender", isActive = true, Position = 3, LogoUrl = "", CountryOfOrigin = "Japan", ContactNumber = "+81-78-795-8888", ManufacturerEmail = "support@ibanez.com", ManufactureAddress = "1-6-10, Tone-cho, Itami City, Hyogo 664-0847, Japan", Descriptions = "A Japanese brand popular among metal and progressive rock players, recognized for its RG and JEM series." },
          //      new Manufacturer { Id = 4, Name = "Yamaha", isActive = true, Position = 3, LogoUrl = "", CountryOfOrigin = "Japan", ContactNumber = " +81-53-460-2311", ManufacturerEmail = "contact@yamaha.com", ManufactureAddress = "ss: 10-1 Nakazawa-cho, Naka-ku, Hamamatsu, Shizuoka 430-8650, Japan", Descriptions = "Offering reliable acoustic and electric guitars, Yamaha is favored by both beginners and professionals for its versatility." },
          //      new Manufacturer { Id = 5, Name = "PRS", isActive = true, Position = 3, LogoUrl = "", CountryOfOrigin = "USA", ContactNumber = "+1-410-643-9970", ManufacturerEmail = ": info@prsguitars.com", ManufactureAddress = "380 Log Canoe Circle, Stevensville, MD 21666, USA", Descriptions = "Known for high-end, finely crafted guitars with a blend of classic and modern design, PRS is prized for its precise sound and build quality." }
          //  );
          //  //ProductType
          //  //modelBuilder.Entity<ProductType>(entity =>
          //  //{
          //  //    entity.Property(e => e.Price)
          //  //      .HasColumnType("decimal(18, 2)");

          //  //entity.HasData(
          //  //    new ProductType { Id = 1, Name = "Cort Ello", Price = 140.99m, isActive = true, Position = 1, ProductStatus = true, ProductSubCategoryId = 1 },
          //  //    new ProductType { Id = 2, Name = "Crafter", Price = 205.50m, isActive = true, Position = 2, ProductStatus = true, ProductSubCategoryId = 1 },
          //  //    new ProductType { Id = 3, Name = "Shredder", Price = 615.75m, isActive = true, Position = 3, ProductStatus = true, ProductSubCategoryId = 2 },
          //  //    new ProductType { Id = 4, Name = "Willow", Price = 152.75m, isActive = true, Position = 4, ProductStatus = true, ProductSubCategoryId = 3 },
          //  //    new ProductType { Id = 5, Name = "Mega MAN", Price = 153.75m, isActive = true, Position = 5, ProductStatus = true, ProductSubCategoryId = 3 }
          //  //);
          //  //    });

          //  // Colors
          //  modelBuilder.Entity<Color>().HasData(
          //      new Color { Id = 1, Name = "Red", isActive = true, Position = 1, Descriptions = "Red as hell" },
          //      new Color { Id = 2, Name = "Blue", isActive = true, Position = 2, Descriptions = "Blues as sky" },
          //      new Color { Id = 3, Name = "Green", isActive = true, Position = 3, Descriptions = "Green as grass" },
          //      new Color { Id = 4, Name = "Yellow", isActive = true, Position = 4, Descriptions = "Yeloow Sabmarine" },
          //      new Color { Id = 5, Name = "Black", isActive = true, Position = 5, Descriptions = "Black is new green" }
          //  );

          //  //Product - Zależna od ProductType i Color, dlatego musi być inicjalizowana po tych tabelach.

          //  //ProductImage - Zależna od ProductType, więc inicjalizowana po niej.

          //   // TechInfos Entries
          //  modelBuilder.Entity<TechInfo>().HasData(
          //      new TechInfo
          //      {
          //          Id = 1,
          //          ProductTypeId = 1,
          //          BodyMaterial = "Mahogany",
          //          NeckMaterial = "Rosewood",
          //          FingerboardMaterial = "Ebony",
          //          Pickups = "Humbucker",
          //          BridgeType = "Tremolo",
          //          Electronics = "Passive",
          //          StringGauge = "0.009-0.042",
          //          TuningMachines = "Locking Tuners",
          //          FretboardRadius = 9.5,
          //          NumberOfFrets = 22,
          //          Finish = "Gloss",
          //          Weight = 75,
          //          AccessoriesIncluded = "N/A"
          //      },
          //      new TechInfo
          //      {
          //          Id = 2,
          //          ProductTypeId = 2,
          //          BodyMaterial = "Alder",
          //          NeckMaterial = "Maple",
          //          FingerboardMaterial = "Maple",
          //          Pickups = "Single Coil",
          //          BridgeType = "Fixed",
          //          Electronics = "Active",
          //          StringGauge = "0.010-0.046",
          //          TuningMachines = "Standard Tuners",
          //          FretboardRadius = 10,
          //          NumberOfFrets = 21,
          //          Finish = "Matte",
          //          Weight = 78,
          //          AccessoriesIncluded = "Gig Bag"
          //      },
          //      new TechInfo
          //      {
          //          Id = 3,
          //          ProductTypeId = 3,
          //          BodyMaterial = "Basswood",
          //          NeckMaterial = "Mahogany",
          //          FingerboardMaterial = "Rosewood",
          //          Pickups = "P90",
          //          BridgeType = "Tune-O-Matic",
          //          Electronics = "Passive",
          //          StringGauge = "0.011-0.049",
          //          TuningMachines = "Vintage Tuners",
          //          FretboardRadius = 12,
          //          NumberOfFrets = 24,
          //          Finish = "Satin",
          //          Weight = 70,
          //          AccessoriesIncluded = "Case"
          //      },
          //      new TechInfo
          //      {
          //          Id = 4,
          //          ProductTypeId = 4,
          //          BodyMaterial = "Ash",
          //          NeckMaterial = "Mahogany",
          //          FingerboardMaterial = "Ebony",
          //          Pickups = "Active Humbucker",
          //          BridgeType = "Floating",
          //          Electronics = "Active",
          //          StringGauge = "0.010-0.052",
          //          TuningMachines = "Locking Tuners",
          //          FretboardRadius = 16,
          //          NumberOfFrets = 22,
          //          Finish = "Gloss",
          //          Weight = 85,
          //          AccessoriesIncluded = "Strap, Picks"
          //      }

          //  );
          //  // FAQ Entries
          //  modelBuilder.Entity<FAQ>().HasData(
          //      new FAQ { Id = 1, ProductTypeId = 1, Email = "john.doe@example.com", Subject = "Shipping Time", Answer = "Shipping usually takes 5-7 business days.", isActive = true, Name = "Shipping Inquiry", Position = 1, Descriptions = "How long does it take to receive an item after purchase?" },
          //      new FAQ { Id = 2, ProductTypeId = 2, Email = "jane.smith@example.com", Subject = "Return Policy", Answer = "You have 30 days to return an item.", isActive = true, Name = "Return Policy", Position = 2, Descriptions = "Can I return my order?" },
          //      new FAQ { Id = 3, ProductTypeId = 3, Email = "alex.johnson@example.com", Subject = "Warranty Period", Answer = "Our products come with a 1-year warranty.", isActive = true, Name = "Warranty Inquiry", Position = 3, Descriptions = "What is the warranty period for products?" },
          //      new FAQ { Id = 4, ProductTypeId = 4, Email = "kate.wilson@example.com", Subject = "Payment Methods", Answer = "We accept all major credit cards and PayPal.", isActive = true, Name = "Payment Inquiry", Position = 4, Descriptions = "What payment methods are available?" },
          //      new FAQ { Id = 5, ProductTypeId = 5, Email = "sam.brown@example.com", Subject = "Product Availability", Answer = "Out-of-stock items are usually restocked within 2 weeks.", isActive = true, Name = "Availability Inquiry", Position = 5, Descriptions = "When will an out-of-stock item be available again?" }
          //  );



            //    base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Tutaj ustaw swoje połączenie do bazy danych
        //    optionsBuilder.UseSqlServer("DefaultConnection");
        //}

        public override int SaveChanges()
        {
            AddPositionNumber();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddPositionNumber();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddPositionNumber()
        {
            var entitiesWithPosition = ChangeTracker.Entries()
    .Where(e => e.State == EntityState.Added && HasProperty(e.Entity, "Position"))
    .ToList();

            foreach (var entry in entitiesWithPosition)
            {
                dynamic entity = entry.Entity;
                var entityType = entity.GetType();
                var dbSet = GetDbSet(entityType);

                int maxPosition = 0;

                foreach (var e in dbSet)
                {
                    int position = (int)e.GetType().GetProperty("Position").GetValue(e);
                    if (position > maxPosition)
                    {
                        maxPosition = position;
                    }
                }

                entity.Position = maxPosition + 1;
            }
        }

        private bool HasProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }



        private IQueryable<dynamic> GetDbSet(Type entityType)
        {
            var method = typeof(MusicStoreContext).GetMethod("Set", new Type[] { }).MakeGenericMethod(entityType);
            return (IQueryable<dynamic>)method.Invoke(this, null);
        }

    }
}
