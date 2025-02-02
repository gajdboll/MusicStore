

using Newtonsoft.Json;

namespace MusicApp.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("productTypeId")]
        public int ProductTypeId { get; set; }
        [JsonProperty("ColorId")]
        public int ColorId { get; set; }

        [JsonProperty("ColorName")]
        public string ColorName { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }
        [JsonProperty("SupplierPrice")]
        public double SupplierPrice { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

         [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

 
    }
}
