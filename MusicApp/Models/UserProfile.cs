using Newtonsoft.Json;
 

namespace MusicApp.Models
{
    public class UserProfile
    {
        [JsonProperty("userId")]  
        public string UserId { get; set; }

        [JsonProperty("userName")]   
        public string UserName { get; set; }
        [JsonProperty("id")]  
        public string id { get; set; }
        [JsonProperty("profilepicture")]
        public string ProfilePicture { get; set; }
    }
}
 
