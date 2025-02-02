 using Newtonsoft.Json;
 
namespace MusicApp.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("categoryImageUrl")]
        public string CategoryImageUrl { get; set; }


        [JsonProperty("subCategories")]
        public List<SubCategory> SubCategories { get; set; }
    }

 
}
