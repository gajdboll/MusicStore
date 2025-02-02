using Newtonsoft.Json;

namespace MusicApp.Models;

public class Token  
{
    [JsonProperty("accesstoken")]
    public string AccessToken { get; set; }

    [JsonProperty("tokentype")]
    public string TokenType { get; set; }
 
}