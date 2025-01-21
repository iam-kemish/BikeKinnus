using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface IOrderHeader:IRepositary<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
        void Save();
    }
}
