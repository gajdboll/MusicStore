using Newtonsoft.Json;

namespace MusicApp.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }  

        [JsonProperty("name")]
        public string Name { get; set; }   

        [JsonProperty("address")]
        public string Address { get; set; }   

        [JsonProperty("totalamount")]
        public double TotalAmount { get; set; }   

        [JsonProperty("applicationUserId")]
        public string ApplicationUserId { get; set; }   

        [JsonProperty("orderDate")]
        public DateTime OrderPlaced { get; set; }  

        [JsonProperty("items")]
        public List<OrderItem> Items { get; set; }  
    }

    public class OrderItem
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }   
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("SuplierPrice")]
        public double SuplierPrice { get; set; }   
    }
}
