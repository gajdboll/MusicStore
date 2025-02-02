using MusicApp.Models;
using Newtonsoft.Json;
 
 public class Color
{
    [JsonProperty("Id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("descriptions")]
    public string Descriptions { get; set; }
}
 