using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Pages;

public partial class ShowProduct : ContentPage
{
    private int productId;
    public Color chosenColor;

    public ShowProduct(int productId)
    {
        InitializeComponent();
        GetProductType(productId);
        this.productId = productId;
    }

    private async void GetProductType(int productId)
    {
        var product = await ApiService.GetShowProduct(productId);

        if (product != null)
        {
            LblProductName.Text = product.Name;
            LblProductDescription.Text = product.Descriptions;
            ImgProduct.Source = product.Image;

            LblProductPrice.Text = $"Supplier Price: {Convert.ToDouble(product.ProductSupplierPrice)} €";
            LblTotalPrice.Text = $"{Convert.ToDouble(product.ProductSupplierPrice)} €";

            var availableColors = product.ProductDetails
                .Select(detail => detail.productColor)
                .ToList();
            ColorPicker.ItemsSource = availableColors;
        }
        else
        {
            await DisplayAlert("Lack of Types", "No matching product", "OK");
            await Navigation.PushAsync(new HomePage());
        }
    }

    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var i = Convert.ToInt32(LblQty.Text);
        i++;
        LblQty.Text = i.ToString();

        var priceText = LblProductPrice.Text.Replace("Supplier Price:", "").Replace("€", "").Trim();
        var totalPrice = i * Convert.ToDouble(priceText);
        LblTotalPrice.Text = totalPrice.ToString("F2") + " €";
    }

    private void BtnRemove_Clicked(object sender, EventArgs e)
    {
        var i = Convert.ToInt32(LblQty.Text);
        i--;
        if (i < 1)
        {
            return;
        }

        LblQty.Text = i.ToString();

        var priceText = LblProductPrice.Text.Replace("Supplier Price:", "").Replace("€", "").Trim();
        var totalPrice = i * Convert.ToDouble(priceText);
        LblTotalPrice.Text = totalPrice.ToString("F2") + " €";
    }

    private async void BtnAddToCart_Clicked(object sender, EventArgs e)
    {
        var shoppingCart = new ShoppingCartRequest()
        {
            Count = Convert.ToInt32(LblQty.Text),
            Price = Convert.ToDouble(LblProductPrice.Text.Replace("Supplier Price:", "").Replace("€", "").Trim()),
            TotalAmount = Convert.ToDouble(LblTotalPrice.Text.Replace("€", "").Trim()),
            ProductTypeId = productId,
            ApplicationUserId = Preferences.Get("id", string.Empty),
            ColorId = chosenColor.Id,
        };

        var response = await ApiService.AddItemsInCart(shoppingCart);
        if (response)
        {
            await DisplayAlert("", "Your item has been added to the cart", "Alright");
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            await DisplayAlert("Oops", "Something went wrong", "Cancel");
        }
    }

    private async void OnColorSelected(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedColor = picker.SelectedItem as Color;

        if (selectedColor != null)
        {
            ColorBox.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse(selectedColor.Name);
            chosenColor = selectedColor;

            var product = await ApiService.GetProductByColor(selectedColor.Id);

            if (product != null && product.Quantity >= 0)
            {
                LblAvailableQuantity.Text = $"Items in stock: {product.Quantity}";
                LblAvailableQuantity.IsVisible = true;
                BtnAddToCart.IsEnabled = true;
            }
            else
            {
                LblAvailableQuantity.Text = "Items in stock: 0";
                LblAvailableQuantity.IsVisible = true;
                BtnAddToCart.IsEnabled = false; // Blokuj dodawanie do koszyka przy braku zapasów
            }
        }
        else
        {
            LblAvailableQuantity.IsVisible = false;
            BtnAddToCart.IsEnabled = false;
        }
    }

        private async void ImgBtnFavorite_Clicked(object sender, EventArgs e)
    {
        var existingBookmark = await ApiService.IsBookmarkedAsync(productId);
        if (existingBookmark == true)
        {
            await ApiService.RemoveBookMarked(productId);
        }
        else
        {
            var bookmarkedProduct = new BookmarkedProduct()
            {
                ProductId = productId,
                IsBookmarked = true,
                Detail = LblProductDescription.Text,
                Name = LblProductName.Text,
                Price = Convert.ToDouble(LblProductPrice.Text.Replace("Supplier Price:", "").Replace("€", "").Trim()),
                ImageUrl = ImgProduct.Source.ToString()
            };

            await ApiService.AddBookmarkAsync(bookmarkedProduct);
        }
        UpdateFavoriteButton();
    }

    private async void UpdateFavoriteButton()
    {
        var existingBookmark = await ApiService.IsBookmarkedAsync(productId);
        if (existingBookmark == true)
        {
            ImgBtnFavorite.Source = "heartfill.png";
        }
        else
        {
            ImgBtnFavorite.Source = "heart.png";
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateFavoriteButton();
    }
}
