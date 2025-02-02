using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStore.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public OrderHeaderRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader obj)
        {
            _context.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStataus = null)
        {
            var orderFromDb = _context.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStataus))
                {
                    orderFromDb.PaymentStatus = paymentStataus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _context.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.IdSession = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }

        }

    }
}
