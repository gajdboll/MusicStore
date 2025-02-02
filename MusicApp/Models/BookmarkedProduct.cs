using Newtonsoft.Json;

namespace MusicApp.Models
{
    public class BookmarkedProduct
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("isBookmarked")]
        public bool IsBookmarked { get; set; }
    }
}
