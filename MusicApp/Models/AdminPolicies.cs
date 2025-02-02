using Newtonsoft.Json;
 

namespace MusicApp.Models
{
    public class AdminPolicies
    {
        public int Id { get; set; }
 
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("descriptions")]
        public string Descriptions { get; set; }

     }
}
