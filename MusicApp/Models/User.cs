using Newtonsoft.Json;

namespace MusicApp.Models;

public class User
{
    [JsonProperty("userid")]
    public int Id { get; set; }

    [JsonProperty("username")]
     public string FirstName { get; set; }
    [JsonProperty("userlastname")]
    public string LastName { get; set; }
    [JsonProperty("useremail")]
    public string Email { get; set; }
    [JsonProperty("userpassword")]
    public string Password { get; set; }
    [JsonProperty("userrole")]
    public string Role { get; set; } = "Customer";

    //public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    //public ICollection<Order> Orders { get; set; }
}