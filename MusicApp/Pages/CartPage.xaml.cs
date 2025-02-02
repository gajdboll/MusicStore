using MusicApp.Models;
using MusicApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MusicApp.Pages;

public partial class CartPage : ContentPage
{
    private ObservableCollection<CartItem> ShoppingCartItems = new ObservableCollection<CartItem>();
    public CartPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetShoppingCartItems();
        var address = Preferences.ContainsKey("address");
        if (address)
        {
            LblAddress.Text = Preferences.Get("address", string.Empty);
        }
        else
        {
            LblAddress.Text = "Provide your Address";

        }
    }
    private async void GetShoppingCartItems()
    {
     
        var shoppingcartitems = await ApiService.GetShoppingCartItems(Preferences.Get("id", string.Empty));

        if (shoppingcartitems == null || shoppingcartitems.CartItems == null || !shoppingcartitems.CartItems.Any())
        {
            await DisplayAlert("Oops", "Empty Shoppping Cart - Please continue your browsing", "OK");
            await Shell.Current.GoToAsync("///home-tab");
            return;  
        }
      
        foreach (var shoppingCart in shoppingcartitems.CartItems)
        {
            ShoppingCartItems.Add(shoppingCart);
        }

        CvCart.ItemsSource = ShoppingCartItems;
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        var totalPrice = ShoppingCartItems.Sum(item => item.SuplierPrice * item.Quantity);
        totalPrice = Math.Round(totalPrice, 2);
        LblTotalPrice.Text = totalPrice.ToString();
    }
    private async void UpdateCartQuantity(int productId, string action)
    {
        var response = await ApiService.UpdateCartQuantity(productId, action);
        if (response)
        {
            return;
        }
        else
        {
            await DisplayAlert("Oops", "Something went wrong", "Cancel");
        }
        UpdateTotalPrice();
    }
    private async void BtnDecrease_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CartItem cartItem)
        {
            if (cartItem.Quantity == 1) return;
            else if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                UpdateTotalPrice();
                UpdateCartQuantity(cartItem.ProductId, "decrease");
                
            }
        }
    }
    private async void BtnIncrease_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CartItem cartItem)
        {

            cartItem.Quantity++;
            UpdateTotalPrice();
            UpdateCartQuantity(cartItem.ProductId, "increase");

        }
    }

    private void BtnDelete_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CartItem cartItem)
        {
            ShoppingCartItems.Remove(cartItem);
            UpdateTotalPrice();
            UpdateCartQuantity(cartItem.ProductId, "delete");
        }
    }
    private void BtnEditAddress_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddressPage());
    }
    private async void ClearCart()
    {
        foreach (var cartItem in ShoppingCartItems.ToList())
        {
            ShoppingCartItems.Remove(cartItem);
            UpdateCartQuantity(cartItem.ProductId, "delete");
        }

        UpdateTotalPrice();
        await DisplayAlert("Order Placed", "Your order has been placed & All items removed from your cart.", "OK");
    }

    private async void TapPlaceOrder_Tapped(object sender, TappedEventArgs e)
    {
        // Build the list of order items based on the shopping cart
        var orderRequest = new Order()
        {
            Items = ShoppingCartItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                SuplierPrice = item.SuplierPrice,
            }).ToList(),
            Address = LblAddress.Text,
            ApplicationUserId = Preferences.Get("id", string.Empty),
            TotalAmount = Convert.ToDouble(LblTotalPrice.Text)
        };

        // Call the API to place the order
        var response = await ApiService.PlaceOrder(orderRequest);

        if (response)
        {
            ClearCart();
        }
        else
        {
            await DisplayAlert("Oops", "Something went wrong", "Cancel");
        }
    }


}