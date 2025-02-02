using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ShoppingCartResponse : INotifyPropertyChanged
{
    [JsonProperty("CartItems")]
    public List<CartItem> CartItems { get; set; }

    [JsonProperty("TotalCartPrice")]
    public double TotalCartPrice { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class CartItem : INotifyPropertyChanged
{
    [JsonProperty("ApplicationUserId")]
    public string ApplicationUserId { get; set; }
    [JsonProperty("productTypeName")]
    public string ProductTypeName { get; set; }
    [JsonProperty("image")]
    public string Image { get; set; }
    [JsonProperty("ProductId")]
    public int ProductId { get; set; }

    [JsonProperty("ProductName")]
    public string ProductName { get; set; }

    [JsonProperty("SuplierPrice")]
    public double SuplierPrice { get; set; }

    [JsonProperty("Color")]
    public string Color { get; set; }

    [JsonProperty("quantity")]
    private int quantity;
    public int Quantity {
        get { return quantity; }
        set
        {
            if (quantity != value)
            {
                quantity = value;
                OnPropertyChanged();
            }

        }
    }

    [JsonProperty("TotalProductPrice")]
    public double TotalProductPrice { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
