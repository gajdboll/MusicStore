using Newtonsoft.Json;

public class Manufacturer
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
}
