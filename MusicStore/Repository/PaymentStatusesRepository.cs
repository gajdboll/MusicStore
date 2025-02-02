using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
using MusicStoreData.Models.CMS;


namespace MusicStore.Repository
{
    public class PaymentStatusesRepository : Repository<PaymentStatuses>, IPaymentStatusesRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public PaymentStatusesRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PaymentStatuses obj)
        {
            _context.Update(obj);
        }
    }

}