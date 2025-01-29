using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class PaymentClass : Repositary<PaymentSummary>, IPayment
    {
        private readonly ApplicationDbContext _Db;
        public PaymentClass(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

        public void Save()
        {
            _Db.SaveChanges();
        }


        public void Update(PaymentSummary paymentSummary)
        {
            _Db.PaymentSummaries.Update(paymentSummary);
        }
    }
}
