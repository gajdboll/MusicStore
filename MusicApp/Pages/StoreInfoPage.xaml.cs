using MusicApp.Services;
using System.Security.Cryptography.X509Certificates;

namespace MusicApp.Pages;

public partial class StoreInfoPage : ContentPage
{
    public StoreInfoPage()
    {
        InitializeComponent();
        LoadStoreData();
    }

    private async void LoadStoreData()
    {
         var storeAddress = await ApiService.GetStoreAddress();
        StoreName.Text = storeAddress.Name;
        StoreAddress.Text = storeAddress.Address;
        StoreAddress2.Text = storeAddress.Address2;
        StorePhone.Text = storeAddress.PhoneNumber;
        StoreEmail.Text = storeAddress.EmailAdres;

         var openingHours = await ApiService.GetOpeningHours();
        OpeningHoursList.ItemsSource = openingHours;
    }
}