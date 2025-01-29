using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface IPayment : IRepositary<PaymentSummary>
    {
        void Update(PaymentSummary paymentSummary);
        void Save();
    }
}
