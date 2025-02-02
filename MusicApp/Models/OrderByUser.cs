using Newtonsoft.Json;
 
namespace MusicApp.Models
{
    public class OrderByUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("totalamount")]
        public double TotalAmount { get; set; }

        [JsonProperty("orderPlaced")]
        public DateTime OrderPlaced { get; set; }
    }
}