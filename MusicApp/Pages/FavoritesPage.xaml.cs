using MusicApp.Models;
using MusicApp.Services;
using System.Diagnostics;

namespace MusicApp.Pages;
public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
    }

    private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkedProduct;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ShowProduct(currentSelection.ProductId));
        ((CollectionView)sender).SelectedItem = null;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetFavoriteProducts();
    }

    private async Task GetFavoriteProducts()
    {
        var favoriteProducts = await ApiService.GetAllBookmarks();

        if (favoriteProducts == null || favoriteProducts.Count == 0)
        {
            await DisplayAlert("Oops", "Empty Wishlist - Please continue your browsing", "OK");
            await Shell.Current.GoToAsync("///home-tab");
            return;
        }

        foreach (var product in favoriteProducts)
        {
            Debug.WriteLine($"Image URL: {product.ImageUrl}");  // Log the image URL
        }
        CvProducts.ItemsSource = favoriteProducts;
    }
}