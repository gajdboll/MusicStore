using MusicStoreData.Models.Users;


namespace MusicStoreData.Models.ShoppingCart
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}
