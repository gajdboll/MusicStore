using MusicStoreData.Models.CMS;
 
namespace MusicStore.Repository.IRepository
{
    public interface IPaymentStatusesRepository : IRepository<PaymentStatuses>
    {
        public void Update(PaymentStatuses obj);
    }
}
