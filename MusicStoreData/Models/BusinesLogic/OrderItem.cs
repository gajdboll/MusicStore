using MusicStoreData.Models.Store;

namespace MusicStoreData.Models.BusinesLogic
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }  
        public double ProductPrice { get; set; }  
        public double ProductSuplierPrice { get; set; }  
        public int Quantity { get; set; }   
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
