using Newtonsoft.Json;
 

namespace MusicApp.Models
{
    public class Concert
    {
        public int Id { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string? Location { get; set; }

        [JsonProperty("musicType")]
        public string? MusicType { get; set; }

        [JsonProperty("openingTime")]
        public DateTime OpeningTime { get; set; }

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        public int Position { get; set; }
    }
}
