using System.ComponentModel.DataAnnotations;
using static MusicStoreData.Controllers.OrderController;

namespace MusicStoreData.Models.BusinesLogic
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } 

    }
}