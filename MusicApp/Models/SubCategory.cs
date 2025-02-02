using MusicApp.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MusicApp.Models
{
    public class SubCategory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("descriptions")]
        public string Descriptions { get; set; }

        [JsonProperty("subCategoryImageUrl")]
        public string SubCategoryUrl { get; set; }

        [JsonProperty("products")]
        public List<ProductTypeList> Products { get; set; }
    }
 
}
