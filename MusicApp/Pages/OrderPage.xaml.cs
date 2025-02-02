using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Pages;

public partial class OrderPage : ContentPage
{

    public OrderPage()
    {
        InitializeComponent();
        GetOrderList();
    }

    private async void GetOrderList()
    {
        var orders = await ApiService.GetOrderByUser(Preferences.Get("id", string.Empty));
        CvOrders.ItemsSource = orders;
    }

    private void CvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Order;
        if (selectedItem == null) return;

        Navigation.PushAsync(new OrderDetailPage(selectedItem.Id, selectedItem.TotalAmount));

        ((CollectionView)sender).SelectedItem = null;
    }
}