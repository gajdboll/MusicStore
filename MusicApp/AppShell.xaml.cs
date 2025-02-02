using MusicApp.Pages;

namespace MusicApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("loading-page", typeof(LoadingPage));
            Routing.RegisterRoute("login-page", typeof(LoginPage));
            Routing.RegisterRoute("signup-page", typeof(SignUpPage));
            Routing.RegisterRoute("home-tab", typeof(HomePage));
            Routing.RegisterRoute("cart-tab", typeof(CartPage));
            Routing.RegisterRoute("order-page", typeof(OrderPage));
            Routing.RegisterRoute("orderdetail-page", typeof(OrderDetailPage));
            Routing.RegisterRoute("admin-policies-page", typeof(AdminPoliciesPage));
            Routing.RegisterRoute("product-detail-page", typeof(ShowProduct));
        }
    }
}
