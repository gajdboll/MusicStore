using MusicApp.Services;
using MusicApp.Models;

namespace MusicApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        GetUserProfile();
        LblUserName.Text = "Logged with " + Preferences.Get("username", string.Empty);
        GetLowStock();
        GetAllSellections();
         GetConcerts();

    }
    private async void GetAllSellections()
	{
        // 2 that endpoint is run when first category like Electric etc is selected - List of Categories returned from db
        var selection = await ApiService.GetShowProductSubCategories();
        CvCategories.ItemsSource = selection;
	}
  
    private async void GetConcerts()
    {
        var allConcerts = await ApiService.GetConcerts();
        var activeConcerts = allConcerts.Where(c => c.IsActive).ToList();
        CvConcerts.ItemsSource = activeConcerts;
    }
 
    private async void GetUserProfile()
    {
        var prd = await ApiService.GetUserProfile();
    }
    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if (currentSelection == null) return;

         Navigation.PushAsync(new ShowProductSubCategories(currentSelection.Id));

         ((CollectionView)sender).SelectedItem = null;
    }
    private void CvConcerts_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Concert;
        if (currentSelection == null) return;

        Navigation.PushAsync(new ConcertPage(currentSelection.Id));

        ((CollectionView)sender).SelectedItem = null;
    }

    private async void GetLowStock()
    {
        var stock = await ApiService.GetLowProductsAsync();  

        if (stock == null || !stock.Any())  
        {
             LblNoLowStockProducts.IsVisible = true;  
            CvLowStock.IsVisible = false;  
        }
        else
        {
             LblNoLowStockProducts.IsVisible = false;  
            CvLowStock.IsVisible = true; 
            CvLowStock.ItemsSource = stock.ToList(); 
        }
    }

    private void CvLowStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ShowProduct(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}