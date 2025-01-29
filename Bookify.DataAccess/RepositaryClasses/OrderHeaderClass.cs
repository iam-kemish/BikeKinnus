using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class OrderHeaderClass : Repositary<OrderHeader>, IOrderHeader
    {
        private readonly ApplicationDbContext _Db;
        public OrderHeaderClass(ApplicationDbContext Db) : base(Db)
        {
            _Db=Db;
        }

        public void Save()
        {
            _Db.SaveChanges();
        }

        public void Update(OrderHeader orderHeader)
        {
           _Db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _Db.OrderHeaders.FirstOrDefault(u=> u.Id == id);
            if(orderFromDb != null) {
                orderFromDb.OrderStatus = orderStatus;
            }
            if(!string.IsNullOrEmpty(paymentStatus)) {
                orderFromDb.PaymentStatus = paymentStatus;
            }
        }

     
    }
}
