 using MusicApp.Services;

namespace MusicApp.Pages;

public partial class OrderDetailPage : ContentPage
{
    public OrderDetailPage(int orderId, double totalPrice)
    {
        InitializeComponent();
        LblTotalPrice.Text = totalPrice + " €";
        GetOrderDetail(orderId);
    }

    private async void GetOrderDetail(int orderId)
    {
        var orderDetails = await ApiService.GetOrderDetails(orderId);
        CvOrderDetail.ItemsSource = orderDetails;

    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///home-tab");
    }
}
