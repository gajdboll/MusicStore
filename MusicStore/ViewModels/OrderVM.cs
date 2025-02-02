using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public OrderHeader OrderHeader{ get; set; }
    }
}
