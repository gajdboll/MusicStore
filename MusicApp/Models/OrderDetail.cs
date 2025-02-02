using Newtonsoft.Json;

namespace MusicApp.Models;

public class OrderDetail
{
    [JsonProperty("productid")]
    public int ProductId { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonProperty("price")]
    public double ProductPrice { get; set; }
    [JsonProperty("SuplierPrice")]
    public double SuplierPrice { get; set; }
    [JsonProperty("ProductName")]

    public string ProductName { get; set; }
    [JsonProperty("ImageURL")]

    public string ImageURL { get; set; }
   
}

  
 