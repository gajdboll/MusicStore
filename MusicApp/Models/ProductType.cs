using Newtonsoft.Json;
 
public class ProductType
{
    [JsonProperty("productTypeId")]
    public int Id { get; set; }

    [JsonProperty("productTypeName")]
    public string Name { get; set; }

    [JsonProperty("productDescriptions")]
    public string Descriptions { get; set; }

    [JsonProperty("productTypePrice")]
    public double Price { get; set; }
    [JsonProperty("ProductSupplierPrice")]
    public double ProductSupplierPrice { get; set; }

    [JsonProperty("productOffer")]
    public bool OnSale { get; set; }

    [JsonProperty("manufacturerName")]
    public string ManufacturerName { get; set; }

    [JsonProperty("productImages")]
    public List<string> ProductImages { get; set; }
    public string Image => ProductImages?.FirstOrDefault() ?? "C:/xampp/htdocs/Images/png/producttype/default.webp";

    [JsonProperty("productDetails")]
    public List<ProductDetail> ProductDetails { get; set; } 
}

public class ProductDetail
{

    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("productName")]
    public string Name { get; set; }

    [JsonProperty("productColor")]
    public Color productColor { get; set; }

    [JsonProperty("productQuantity")]
    public int Quantity { get; set; }
}
