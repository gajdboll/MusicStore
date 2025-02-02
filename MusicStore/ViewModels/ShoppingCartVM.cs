using MusicStoreData.Models.Basket;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader{ get; set; }
        public DiscountCode DiscountCode { get; set; }
        public decimal DiscountedTotal { get; set; }
        public string DiscountMessage { get; internal set; }
    }
}
