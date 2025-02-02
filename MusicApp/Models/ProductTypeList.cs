
 using Newtonsoft.Json;
public class ProductTypeList
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Descriptions { get; set; }
    [JsonProperty("price")]
    public double Price { get; set; }
    [JsonProperty("SupplierPrice")]
    public double SupplierPrice { get; set; }

    [JsonProperty("onSale")]
    public bool OnSale { get; set; }

    [JsonProperty("manufacturerName")]
    public string ManufacturerName { get; set; }

    [JsonProperty("images")]
    public List<string> Images { get; set; }
    public string Image => Images?.FirstOrDefault() ?? "C:/xampp/htdocs/Images/png/producttype/default.webp";

 
}
 

