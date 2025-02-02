using Newtonsoft.Json;
 

 
namespace MusicApp.Models
{
    public class ShoppingCartRequest
    {
        [JsonProperty("colorId")]
        public int ColorId { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("totalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty("productTypeId")]
        public int ProductTypeId { get; set; }

        [JsonProperty("applicationUserId")]
        public string ApplicationUserId { get; set; }
    }
}
